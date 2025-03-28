using System.Net;
using System.Text.Json;

namespace Smudging.src.entity
{
    public class RequestBody
    {
        // 获取请求体数据
        private string _data = "";
        private readonly string contentType = "application/json";
        // 请求复杂度标头
        private readonly string levelHeader = "X-Request-Level";
        // 请求复杂度
        private RequestLevel _level = RequestLevel.Low;
        // 请求是否可用
        private bool _ok = false;
        // 错误信息
        private string _error = "";

        // 响应码
        private int _code = ResponseStatus.OK;

        public string Data => _data;

        public bool Ok => _ok;

        public string Error => _error;

        public int Code => _code;

        public RequestLevel Level => _level;

        // 验证请求头是否为JSON格式，执行顺序 1
        public string? RequestHeaderIsJSON(HttpListenerRequest request)
        {
            try
            {
                if (request?.ContentType != null && request.ContentType.Contains(contentType, StringComparison.OrdinalIgnoreCase))
                {
                    // 从请求流读取 JSON 数据
                    using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        _data = reader.ReadToEnd();
                    }

                    _ok = true;
                    return null;
                }
                else
                {
                    _ok = false;
                    _error = "只支持 JSON 格式数据请求，拒绝请求！";
                    _code = ResponseStatus.FORBIDDEN;
                    return _error;
                }
            }
            catch (Exception e)
            {
                _ok = false;
                _code = ResponseStatus.ERROR;
                _error = $"请求解析失败: {e.Message}";
                return _error;
            }
        }

        // 确认请求等级，执行顺序 last
        public string? RequestHeaderConfirmLevel(HttpListenerRequest request)
        {
            try
            {
                // 获取所有请求头
                var headers = request?.Headers;
                // 获取请求等级
                if (headers == null || !headers.AllKeys.Contains(levelHeader, StringComparer.OrdinalIgnoreCase))
                {
                    _ok = false;
                    _error = "缺少 X-Request-Level 请求头，拒绝请求！";
                    _code = ResponseStatus.FORBIDDEN;
                    return _error;
                }

                // 验证请求头
                if (int.TryParse(headers[levelHeader], out int heardLevel) &&
                    Enum.IsDefined(typeof(RequestLevel), heardLevel))
                {
                    _level = (RequestLevel)heardLevel;
                    _ok = true;
                    return null;
                }
                else
                {
                    _ok = false;
                    _error = "X-Request-Level 请求头中指定的等级无效，请检查是否在范围内，拒绝请求！";
                    _code = ResponseStatus.BAD_REQUEST;
                    Console.WriteLine(_error);
                    return _error;
                }
            }
            catch (Exception e)
            {
                _ok = false;
                _code = ResponseStatus.ERROR;
                _error = $"请求头解析失败: {e.Message}";
                return _error;
            }
        }

        // 将请求的JSON字符串反序列化成对象
        public Dictionary<string, string> JSONStringToDeserialization()
        {
            try
            {
                // 如果请求的数据为空，则返回空对象
                if (_data == null || _data == "")
                {
                    return [];
                }
                // 使用 JsonDocument 进行调试
                using JsonDocument doc = JsonDocument.Parse(_data);

                Dictionary<string, string> parameters = [];
                foreach (JsonProperty prop in doc.RootElement.EnumerateObject())
                {
                    string key = prop.Name;

                    if (prop.Value.ValueKind == JsonValueKind.String)
                    {
                        string value = prop.Value.GetString() ?? "";
                        parameters[key] = value;
                    }
                    else if (prop.Value.ValueKind == JsonValueKind.Number)
                    {
                        if (prop.Value.TryGetInt32(out int intValue))
                        {
                            parameters[key] = intValue.ToString();
                        }
                        else
                        {
                            throw new InvalidOperationException($"无法将 JSON 值 {key} 转换为整数");
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException($"无法处理类型为 {prop.Value.ValueKind} 的值");
                    }
                }

                return parameters;
            }
            catch (JsonException e)
            {
                // 捕获 JSON 反序列化异常并记录错误
                _ok = false;
                _code = ResponseStatus.ERROR;
                _error = $"JSON 请求参数反序列化失败: {e.Message}";
                Console.WriteLine(_error);
                return [];
            }
            catch (Exception e)
            {
                // 捕获其他异常并记录错误
                _ok = false;
                _code = ResponseStatus.ERROR;
                _error = $"请求参数反序列化失败: {e.Message}";
                Console.WriteLine(_error);
                return [];
            }
        }

        // 将 HTTP 请求方法转换为 RequestMethod 枚举
        public static RequestMethod ConvertRequestMethod(string httpMethod)
        {
            // 获取请求方法并转换为 RequestMethod 枚举
            if (Enum.TryParse(httpMethod, true, out RequestMethod requestMethod))
            {
                return requestMethod;
            }
            throw new Exception($"无效请求方式: {httpMethod}");
        }
    }

    // 请求复杂度
    // low 表示可快速执行无需开启新线程
    // medium 表示任务具有一定复杂度，需要进入任务队列进行执行
    // high 表示需要开启新线程并且需要快速执行
    public enum RequestLevel
    {
        // 低
        Low,
        // 中
        Medium,
        // 高
        High
    }

}