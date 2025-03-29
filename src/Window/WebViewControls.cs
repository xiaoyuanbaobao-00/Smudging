using Microsoft.Web.WebView2.WinForms;
namespace Smudging.src.Window
{
    public class WebViewControls {
        // WebView2 控件
        private static WebView2? Control;

        /// <summary>
        /// WebView2 控件
        /// </summary>
        public static WebView2 WebView {
            get {
                if(Control == null) {
                    throw new Exception("WebView2 控件为空");
                }
                return Control;
            }

            set {
                Control = value;
            }
        }

        /// <summary>
        /// 获取 WebView2 控件显示所需的 url
        /// </summary>
        /// <param name="url"></param>
        public void SetWebSource(string url)
        {
            WebView?.InvokeIfRequired(() =>
            {
                // 设置 WebView2 控件显示所需的 url
                WebView.Source = new Uri(url);
            });
        }
    }
}