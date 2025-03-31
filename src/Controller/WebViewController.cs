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
        [ApiCustom("/source", RequestMethod.POST)]
        public ResponseBody SetUrl(string url)
        {
            HttpServer.WebControls?.SetWebSource(url);
            return new ResponseBody();
        }

        /// <summary>
        /// 刷新网页
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/reload", RequestMethod.POST)]
        public ResponseBody Reload()
        {
            HttpServer.WebControls?.Reload();
            return new ResponseBody();
        }

        /// <summary>
        /// 注入脚本
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/script", RequestMethod.POST)]
        public ResponseBody ExecuteScriptAsync(string script)
        {
            HttpServer.WebControls?.ExecuteScriptAsync(script);
            return new ResponseBody();
        }

    }
}