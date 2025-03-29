using Smudging.src.entity;
using Smudging.src.Server;

namespace Smudging.src.Controller
{
    /// <summary>
    /// WebView控制器
    /// </summary>
    [ApiCustom("/webview")]
    public class WebViewController
    {
        /// <summary>
        /// 设置 WebView 源
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [ApiCustom("/source", RequestMethod.GET)]
        public ResponseBody SetUrl(string url)
        {
            HttpServer.CONTROLS?.SetWebSource(url);
            return new ResponseBody(ResponseStatus.OK, null, "执行成功！");
        }
    }
}