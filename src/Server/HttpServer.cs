using Smudging.src.CustomizeException;
using Smudging.src.entity;
using Smudging.src.Window;
using System.Net;
using System.Text;

namespace Smudging.src.Server
{
    public class HttpServer
    {
        // 参数
        private static string[]? args;
        // 端口
        private int port = 9902;
        // 地址
        private readonly string address = "localhost";
        // 监听
        private readonly HttpListener listener;
        // 队列处理任务
        private readonly static QueueProcessTask queueProcessTask = QueueProcessTask.Instance();
        // 扫描 API
        private readonly static ApiScanner apiScanner = ApiScanner.Instance();

        /// <summary>
        /// 窗体控制对象
        /// </summary>
        public static WinControls? CONTROLS = null;

        /// <summary>
        /// WebView 控制对象
        /// </summary>
        public static WebViewControls? WebControls = null;

        public HttpServer()
        {
            // 获取参数
            args = Environment.GetCommandLineArgs();

            // 清除参数并获取端口
            //AnalyzeArgs();

            // 创建监听
            listener = new HttpListener();
            listener.Prefixes.Add($"http://{address}:{port}/");
        }

        // 解析参数并获取端口
        private void AnalyzeArgs()
        {
            if (args != null && args.Length > 1)
            {
                args = RemoveArrayIndexOf(args!, 0);
                if (!int.TryParse(args[0], out port))
                {
                    MyException.ErrorException("端口数据转换失败！");
                }
            }
            else
            {
                args = [];
            }
        }

        // 通过索引移除数组元素
        public static string[] RemoveArrayIndexOf(string[] array, int index)
        {
            string[] newArray = new string[array.Length - 1];
            int newIndex = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (i != index)
                {
                    newArray[newIndex] = array[i];
                    newIndex++;
                }
            }

            return newArray;
        }

        /// <summary>
        /// 启动服务器
        /// </summary>
        /// <returns></returns>
        public async Task StartAsync()
        {
            listener.Start();
            Console.WriteLine($"Server started on http://{address}:{port}/");
            while (true)
            {
                var context = await listener.GetContextAsync();
                ProcessRequest(context);
            }
        }

        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="context"></param>
        private static void ProcessRequest(HttpListenerContext context)
        {
            // 获取请求和响应
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            // 检查 request.Url 是否为 null
            if (request.Url == null)
            {
                // 返回错误响应
                ProcessResponse(response, new ResponseBody(ResponseStatus.BAD_REQUEST, "Invalid request: URL is null"));
                return;
            }

            // 获取请求路径
            string requestPath = request.Url.AbsolutePath;

            // 设置 CORS 头
            response.Headers.Add("Access-Control-Allow-Origin", "*"); // 允许所有域名访问
            response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, HEAD, PATCH, OPTIONS");
            response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");

            // 处理预检请求（OPTIONS）
            if (request.HttpMethod == "OPTIONS")
            {
                response.StatusCode = ResponseStatus.OK;
                response.Close();
                return;
            }

            // 创建请求体
            RequestBody requestBody = VerifyRequest(request);

            // 验证请求参数是否通过
            if (!requestBody.Ok)
            {
                ProcessResponse(response, new ResponseBody(requestBody.Code, requestBody.Error));
                return;
            }

            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}，请求路由：{requestPath}, 收到 JSON 数据: \n" + requestBody.Data);

            // 获取请求数据
            Dictionary<string, string>? keyValuePairs = requestBody.JSONStringToDeserialization();
            // 处理请求
            HandleApi(request, response, requestPath, requestBody.Level, keyValuePairs);
        }

        /// <summary>
        /// 验证请求是否有效
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static RequestBody VerifyRequest(HttpListenerRequest request)
        {
            // 创建请求体
            RequestBody requestBody = new();

            // 验证请求头是否为JSON格式，执行顺序 1
            if (requestBody.RequestHeaderIsJSON(request) != null)
            {
                Console.WriteLine(requestBody.Error);
                return requestBody;
            }

            // 确认请求等级，执行顺序 2
            if (requestBody.RequestHeaderConfirmLevel(request) != null)
            {
                Console.WriteLine(requestBody.Error);
                return requestBody;
            }
            return requestBody;
        }

        /// <summary>
        /// 停止服务器
        /// </summary>
        public void Stop()
        {
            // 停止队列处理任务
            queueProcessTask.Stop();
            // 停止监听请求，并终止所有正在处理的请求
            listener.Stop();
            // 关闭监听器
            listener.Close();
        }

        /// <summary>
        /// 处理API请求
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="requestPath"></param>
        /// <param name="level"></param>
        /// <param name="keyValuePairs"></param>
        private static void HandleApi(HttpListenerRequest request, HttpListenerResponse response, string requestPath, RequestLevel level, Dictionary<string, string> keyValuePairs)
        {
            // 外层响应体
            ResponseBody resp = new(ResponseStatus.OK, null, "");
            try
            {
                // 获取请求方法
                RequestMethod requestMethod = RequestBody.ConvertRequestMethod(request.HttpMethod);

                // 确认请求等级
                if (RequestLevel.Low == level)
                { // 同步执行任务
                    ProcessResponse(response, ResponseBody.MergeRespnseBody(resp, apiScanner, requestPath, requestMethod, keyValuePairs));
                    return;
                }
                else if (RequestLevel.Medium == level)
                { // 添加到队列中执行任务
                    queueProcessTask.AddTask(() => Task.Run(() => ProcessResponse(response, ResponseBody.MergeRespnseBody(resp, apiScanner, requestPath, requestMethod, keyValuePairs))));
                    return;
                }
                else if (RequestLevel.High == level)
                { // 启动线程执行任务
                    QueueProcessTask.StartThreadTask(() =>
                    {
                        ProcessResponse(response, ResponseBody.MergeRespnseBody(resp, apiScanner, requestPath, requestMethod, keyValuePairs));
                    });
                    return;
                }
                throw new Exception("未匹配到正确的响应等级");
            }
            catch (Exception e)
            {
                resp.Code = ResponseStatus.ERROR;
                resp.Message = e.Message;
                Console.WriteLine(e.Message);
                ProcessResponse(response, resp);
            }
        }

        // 返回响应
        private static void ProcessResponse(HttpListenerResponse response, ResponseBody responseBody)
        {
            // 设置响应头和响应体
            response.StatusCode = responseBody.Code;
            // 设置响应体
            response.ContentType = "application/json";
            string result = responseBody.ToJSONString();
            // 设置响应内容
            byte[] buffer = Encoding.UTF8.GetBytes(result);
            // 设置响应长度
            response.ContentLength64 = buffer.Length;
            response.AddHeader("Access-Control-Allow-Origin", "*");
            // 写入响应
            using var output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
        }

    }
}
