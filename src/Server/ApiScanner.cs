using System.Reflection;
using System.Text.RegularExpressions;
using Smudging.src.entity;

namespace Smudging.src.Server
{
    /// <summary>
    /// 扫描所有的 ApiCustom 注解类
    /// </summary>
    public partial class ApiScanner
    {

        private readonly Dictionary<string, Dictionary<string, RouteInfo>> _routeMap = [];

        private static readonly ApiScanner _instance = new();

        // 指定要扫描的命名空间
        private readonly string targetNamespace = "Smudging.src.Controller";

        private ApiScanner()
        {
            Console.WriteLine("开始扫描 ApiCustomAttribute 标注的类...");
            ScanApiCustomClass();
        }

        /// <summary>
        /// 获取 ApiScanner 单例
        /// </summary>
        /// <returns></returns>
        public static ApiScanner Instance()
        {
            return _instance;
        }

        // 扫描所有 ApiCustomAttribute 注解的类
        private void ScanApiCustomClass()
        {
            // 获取当前程序集
            Assembly assembly = Assembly.GetExecutingAssembly();

            // 获取所有类型
            Type[] types = assembly.GetTypes();

            // 筛选出指定命名空间中的类型，并检查是否被 ApiCustomAttribute 修饰
            IEnumerable<Type>? attributedTypes = types
                .Where(t => t.Namespace == targetNamespace)
                .Where(t => t.GetCustomAttributes(typeof(ApiCustomAttribute), false).Length != 0);

            // 遍历筛选后的类型
            foreach (Type type in attributedTypes)
            {
                // 获取类的 ApiCustomAttribute 属性
                ApiCustomAttribute? classAttribute = type.GetCustomAttribute<ApiCustomAttribute>();
                // 如果类被 ApiCustomAttribute 修饰，则获取类的路径和请求方法
                if (classAttribute != null)
                {
                    // 获取类的路径
                    string classPath = classAttribute.Path;

                    // 检查类型是否有公共无参构造函数
                    if (type.GetConstructor(Type.EmptyTypes) == null)
                    {
                        throw new InvalidOperationException($"类型 {type.FullName} 缺少公共无参构造函数。");
                    }

                    // 创建类的实例
                    object instance = Activator.CreateInstance(type)!; // 使用 ! 运算符告知编译器此处不会为 null

                    // 获取类的所有方法
                    var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);

                    // 遍历类的所有方法，检查是否被 ApiCustomAttribute 修饰
                    foreach (var method in methods)
                    {
                        // 获取方法的 ApiCustomAttribute 属性
                        ApiCustomAttribute? methodAttribute = method.GetCustomAttribute<ApiCustomAttribute>();
                        // 如果方法被 ApiCustomAttribute 修饰，则获取方法的路径和请求方法
                        if (methodAttribute != null)
                        {
                            // 获取方法的路径和请求方法
                            string methodPath = methodAttribute.Path;
                            // 获取方法的请求方法
                            RequestMethod requestMethod = methodAttribute.Method;

                            // 构建完整的路径
                            string fullPath = MyRegex().Replace($"/smudging{classPath}{methodPath}".TrimEnd('/'), "/");
                            
                            Console.WriteLine($"扫描到路由：{fullPath}");

                            // 获取方法的参数信息
                            ParameterInfo[] parameters = method.GetParameters();
                            // 创建参数列表
                            List<ParameterInfo> parameterList = [.. parameters];

                            // 创建 RouteInfo 对象
                            RouteInfo routeInfo = new()
                            {
                                Instance = instance,
                                MethodInfo = method,
                                Parameters = parameterList,
                                RequestMethod = requestMethod
                            };

                            // 如果路由映射中不存在该路径，则创建一个空的字典
                            if (!_routeMap.TryGetValue(fullPath, out Dictionary<string, RouteInfo>? value))
                            {
                                // 创建一个空的字典
                                value = [];
                                // 将字典添加到路由映射中
                                _routeMap[fullPath] = value;
                            }

                            // 将方法添加到字典中
                            value[requestMethod.ToString()] = routeInfo;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="path">访问的 url 路径</param>
        /// <param name="method">访问的方式</param>
        /// <param name="parameters">访问的参数数据</param>
        /// <returns>返回的处理结果</returns>
        /// <exception cref="ArgumentException"></exception>
        public object HandleRequest(string path, RequestMethod method, Dictionary<string, string> parameters)
        {
            // 检查路由映射中是否存在该路径和该请求方式
            if (_routeMap.TryGetValue(path, out var methods) && methods.TryGetValue(method.ToString(), out var routeInfo))
            {

                // 获取方法参数
                var methodParameters = routeInfo.Parameters ?? [];
                // 创建参数数组
                var methodArgs = new object[methodParameters.Count];

                // 检查是否有参数
                if (methodParameters.Count > 0)
                {
                    // 遍历参数，将参数值转换为参数类型
                    for (int i = 0; i < methodParameters.Count; i++)
                    {
                        // 获取参数信息
                        var paramInfo = methodParameters[i];

                        // 检查参数名称是否为 null
                        if (paramInfo.Name == null)
                        {
                            throw new InvalidOperationException($"方法参数 {paramInfo.ParameterType.Name} 的名称为空。");
                        }

                        // 检查参数是否存在
                        if (parameters.TryGetValue(paramInfo.Name, out var paramValue))
                        {
                            // 将参数值转换为参数类型
                            methodArgs[i] = Convert.ChangeType(paramValue, paramInfo.ParameterType);
                        }
                        else
                        {
                            return new ResponseBody(ResponseStatus.FORBIDDEN, null, $"缺少参数: {paramInfo.Name}");
                        }
                    }
                }

                object? result = null;
                try{
                    // 调用 api 方法
                    result = routeInfo.MethodInfo!.Invoke(routeInfo.Instance, methodArgs);
                }catch(Exception e){
                    return new ResponseBody(ResponseStatus.ERROR, null, e.Message);
                }

                // 调用方法
                return result!;
            }
            else
            {
                Console.WriteLine("未找到匹配的路由。");
                return new ResponseBody(ResponseStatus.NOT_FOUND, null, "404 Not Found.");
            }
        }

        [GeneratedRegex("/+")]
        private static partial Regex MyRegex();
    }
}