namespace Smudging.src.Window
{
    public class WinControls(Form form)
    {
        // 窗体
        private readonly Form _form = form;

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
        public void Unminimize()
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
        /// 获取窗体是否处于隐藏状态，true 隐藏，false 显示
        /// </summary>
        /// <returns>窗体是否隐藏</returns>
        public bool IsVisible()
        {
            return _form.InvokeIfRequired(() => !_form.Visible);
        }

        /// <summary>
        /// 窗体置顶
        /// </summary>
        public void TopMost() {
            _form.InvokeIfRequired(() =>
            {
                _form.TopMost = true;
            });
        }

        /// <summary>
        /// 取消窗体置顶
        /// </summary>
        public void TopMostCancel() {
            _form.InvokeIfRequired(() =>
            {
                _form.TopMost = false;
            });
        }

        /// <summary>
        /// 激活窗体
        /// </summary>
        public void Activate() {
            _form.InvokeIfRequired(() =>
            {
                _form.Activate();
            });
        }

        /// <summary>
        /// 聚焦窗口
        /// </summary>
        public void SetFocus() {
            _form.InvokeIfRequired(() =>
            {
                // 如果窗体处于最小化状态，则先将其恢复为正常状态
                if(_form.WindowState == FormWindowState.Minimized) {
                    _form.WindowState = FormWindowState.Normal;
                }

                // 显示窗体并置顶
                _form.TopMost = true;
                // 聚焦窗体
                _form.Focus();
                // 取消窗体置顶
                _form.TopMost = false;
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
