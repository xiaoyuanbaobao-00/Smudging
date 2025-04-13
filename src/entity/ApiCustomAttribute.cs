using System.Reflection;

namespace Smudging.src.entity
{
    /// <summary>
    /// 自定义标记，用于定义路由与请求方式
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiCustomAttribute : Attribute
    {
        /// <summary>
        /// 路由
        /// </summary>
        private string _path = "/";

        /// <summary>
        /// 请求方法
        /// </summary>
        private RequestMethod _method = RequestMethod.GET;

        public string Path
        {
            get => _path;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Path 不能为 null。");
                }

                // 去除首尾空格，并判断是否为空字符串或"/"
                string val = value.Trim();
                if (!(val == "" || val == "/"))
                {
                    // 如果首字符不是"/"，则添加"/"
                    if (!val.StartsWith('/'))
                    {
                        _path = '/' + val;
                    }
                    else
                    {
                        _path = val;
                    }
                }
            }
        }

        public RequestMethod Method
        {
            get => _method;
            set
            {
                if (Enum.IsDefined(typeof(RequestMethod), value))
                {
                    _method = value;
                }
                else
                {
                    throw new ArgumentException("Method 请求方式不是 GET、POST、PUT、DELETE、HEAD、OPTIONS 或 PATCH。");
                }
            }
        }

        public ApiCustomAttribute(string path)
        {
            Path = path;
        }

        public ApiCustomAttribute(string path, RequestMethod method = RequestMethod.GET)
        {
            Path = path;
            Method = method;
        }
    }

    /// <summary>
    /// 请求类型
    /// </summary>
    public enum RequestMethod
    {
        GET,
        POST,
        PUT,
        DELETE,
        HEAD,
        OPTIONS,
        PATCH
    }

    // 路由信息
    public class RouteInfo
    {
        public MethodInfo? MethodInfo { get; set; }
        public List<ParameterInfo>? Parameters { get; set; }
        public RequestMethod RequestMethod { get; set; }
    }

    
}