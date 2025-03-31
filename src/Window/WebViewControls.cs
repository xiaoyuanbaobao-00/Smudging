using Microsoft.Web.WebView2.WinForms;
namespace Smudging.src.Window
{
    public class WebViewControls
    {
        // WebView2 控件
        private static WebView2? Control;

        // WebView2 控件显示所需的 url
        private string _sources = "";

        /// <summary>
        /// WebView2 控件
        /// </summary>
        public static WebView2 WebView
        {
            get
            {
                if (Control == null)
                {
                    throw new Exception("WebView2 控件为空");
                }
                return Control;
            }

            set { Control = value; }
        }

        /// <summary>
        /// 获取或设置 WebView2 控件显示所需的 url
        /// </summary>
        public string Sources
        {
            get { return _sources; }

            set
            {
                if (value == null)
                {
                    throw new ArgumentException("WebView2 控件导航所需的 url 不能为空。");
                }

                if (value.StartsWith("http://") || value.StartsWith("https://"))
                {
                    _sources = value;
                }
                else
                {
                    throw new Exception("WebView2 控件导航所需的 url 必须以 http:// 或 https:// 开头。");
                }
            }
        }

        /// <summary>
        /// 获取 WebView2 控件显示所需的 url
        /// </summary>
        /// <param name="url"></param>
        public void SetWebSource(string url)
        {
            Sources = url;
            WebView?.InvokeIfRequired(() =>
            {
                // 设置 WebView2 控件显示所需的 url
                WebView.Source = new Uri(Sources);
            });
        }

        /// <summary>
        /// 向页面注入脚本
        /// </summary>
        /// <param name="script"></param>
        public void ExecuteScriptAsync(string script)
        {
            if (Sources.StartsWith("https://"))
            {
                throw new Exception("注入脚本失败，WebView2 控件导航所需的 url 必须以 http:// 开头才允许注入。");
            }
            WebView?.InvokeIfRequired(() =>
            {
                WebView.ExecuteScriptAsync(script).Wait();
            });
        }

        /// <summary>
        /// 刷新页面
        /// </summary>
        public void Reload()
        {
            WebView?.InvokeIfRequired(() =>
            {
                WebView.Reload();
            });
        }

    }
}