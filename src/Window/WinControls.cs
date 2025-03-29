namespace Smudging.src.Window
{
    public class WinControls(Form form)
    {
        // 窗体
        private readonly Form _form = form;

        // WebView2 控件
        public static Microsoft.Web.WebView2.WinForms.WebView2? WebView;

        /// <summary>
        /// 显示窗体
        /// </summary>
        public void Show()
        {
            _form.InvokeIfRequired(() =>
            {
                _form.Show();
            });
        }

        /// <summary>
        /// 隐藏窗体
        /// </summary>
        public void Hide()
        {
            _form.InvokeIfRequired(() =>
            {
                _form.Hide();
            });
        }

        /// <summary>
        /// 最大化窗体
        /// </summary>
        public void Maximize()
        {
            _form.InvokeIfRequired(() =>
            {
                _form.WindowState = FormWindowState.Maximized;
            });
        }

        /// <summary>
        /// 最小化窗体
        /// </summary>
        public void Minimize()
        {
            _form.InvokeIfRequired(() =>
            {
                _form.WindowState = FormWindowState.Minimized;
            });
        }

        /// <summary>
        ///  窗口化状态
        /// </summary>
        public void Normal()
        {
            _form.InvokeIfRequired(() =>
            {
                _form.WindowState = FormWindowState.Normal;
            });
        }

        /// <summary>
        /// 获取窗体的标题
        /// </summary>
        /// <returns>窗体的标题</returns>
        public string GetTitle()
        {
            return _form.InvokeIfRequired(() => _form.Text);
        }

        /// <summary>
        /// 设置窗体的标题
        /// </summary>
        /// <param name="title">新的标题</param>
        public void SetTitle(string title)
        {
            _form.InvokeIfRequired(() =>
            {
                _form.Text = title;
            });
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

    public static class ControlExtensions
    {
        /// <summary>
        /// 控件 InvokeRequired 扩展（无参数，无返回值）
        /// </summary>
        /// <param name="control"></param>
        /// <param name="action"></param>
        public static void InvokeIfRequired(this Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }

        /// <summary>
        /// 控件 InvokeRequired 扩展（有参数，有返回值）
        /// </summary>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="control"></param>
        /// <param name="func"></param>
        /// <returns>返回值</returns>
        public static TResult InvokeIfRequired<TResult>(this Control control, Func<TResult> func)
        {
            if (control.InvokeRequired)
            {
                return control.Invoke(func);
            }
            else
            {
                return func();
            }
        }

        /// <summary>
        /// 控件 InvokeRequired 扩展（有参数，有返回值）
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="control"></param>
        /// <param name="func"></param>
        /// <param name="arg">参数</param>
        /// <returns>返回值</returns>
        public static TResult InvokeIfRequired<T, TResult>(this Control control, Func<T, TResult> func, T arg)
        {
            if (control.InvokeRequired)
            {
                return (TResult)control.Invoke(func, [arg]);
            }
            else
            {
                return func(arg);
            }
        }
    }

}
