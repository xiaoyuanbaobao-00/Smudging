using System.Text.Json;
using System.Text.Json.Serialization;
using Smudging.src.Server;

namespace Smudging.src.entity
{
    // 响应对象
    public class ResponseBody
    {

        private static readonly JsonSerializerOptions jsonSerializerOptions = new()
        {
            IncludeFields = true
        };

        [JsonPropertyName("code")]
        public int Code { get; set; } = ResponseStatus.OK;

        [JsonPropertyName("data")]
        public object? Data { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = "执行成功！";

        public ResponseBody() { }

        public ResponseBody(int code, object? data, string message)
        {
            Code = code;
            Data = data;
            Message = message;
        }

        public ResponseBody(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public ResponseBody(object data)
        {
            Data = data;
        }

        /// <summary>
        /// 将响应对象转换为JSON字符串
        /// </summary>
        /// <returns>string</returns>
        public string ToJSONString()
        {
            // 使用 JsonSerializer 进行序列化
            return JsonSerializer.Serialize(this, jsonSerializerOptions);
        }

        /// <summary>
        /// 将响应对象与请求对象进行合并
        /// </summary>
        /// <param name="resp"></param>
        /// <param name="requestPath"></param>
        /// <param name="requestMethod"></param>
        /// <param name="keyValuePairs"></param>
        /// <returns></returns>
        public static ResponseBody MergeRespnseBody(ResponseBody resp, ApiScanner apiScanner, string requestPath, RequestMethod requestMethod, Dictionary<string, string> keyValuePairs)
        {
            // 调用 ApiScanner 的 HandleRequest 方法，执行 api 方法
            ResponseBody result = (ResponseBody)apiScanner.HandleRequest(requestPath, requestMethod, keyValuePairs);
            resp.Data = result.Data;
            resp.Code = result.Code;
            resp.Message = result.Message;
            return resp;
        }
    }

    /// <summary>
    /// 响应状态码
    /// </summary>
    public class ResponseStatus
    {
        /// <summary>
        /// （成功）服务器已成功接收到，并理解请求
        /// </summary>
        public static readonly int OK = 200;
        /// <summary>
        /// 未发现该资源
        /// </summary>
        public static readonly int NOT_FOUND = 404;
        /// <summary>
        /// （错误请求) 服务器不理解请求的语法。
        /// </summary>
        public static readonly int BAD_REQUEST = 400;
        /// <summary>
        /// (未授权) 请求要求身份验证。 
        /// </summary>
        public static readonly int UNAUTHORIZED = 401;
        /// <summary>
        /// （禁止）服务器拒绝请求。
        /// </summary>
        public static readonly int FORBIDDEN = 403;
        /// <summary>
        /// （错误）服务器遇到错误，无法完成请求。
        /// </summary>
        public static readonly int ERROR = 500;
    }
}