namespace Smudging.src.Window
{
    /// <summary>
    /// 用于控制窗体的行为
    /// </summary>
    /// <param name="form"></param>
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
        /// 关闭窗体
        /// </summary>
        public void Close()
        {
            _form.InvokeIfRequired(() =>
            {
                _form.Close();
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
        public void TopMost()
        {
            _form.InvokeIfRequired(() =>
            {
                _form.TopMost = true;
            });
        }

        /// <summary>
        /// 取消窗体置顶
        /// </summary>
        public void TopMostCancel()
        {
            _form.InvokeIfRequired(() =>
            {
                _form.TopMost = false;
            });
        }

        /// <summary>
        /// 激活窗体
        /// </summary>
        public void Activate()
        {
            _form.InvokeIfRequired(() =>
            {
                _form.Activate();
            });
        }

        /// <summary>
        /// 聚焦窗口
        /// </summary>
        public void SetFocus()
        {
            _form.InvokeIfRequired(() =>
            {
                // 如果窗体处于最小化状态，则先将其恢复为正常状态
                if (_form.WindowState == FormWindowState.Minimized)
                {
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

        /// <summary>
        /// 设置窗口大小
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetWindowSize(int width, int height)
        {
            _form.InvokeIfRequired(() =>
            {
                _form.Size = new Size(width, height);
            });
        }

        /// <summary>
        /// 获取窗口大小
        /// </summary>
        /// <returns> (width, height) => ValueTuple&lt;int, int&gt;?</returns>
        public (int, int) GetWindowSize()
        {
            int width = 0;
            int height = 0;
            return _form.InvokeIfRequired(() =>
            {
                width = _form.Width;
                height = _form.Height;
                return (width, height);
            });
        }

        /// <summary>
        /// 设置窗口位置
        /// </summary>
        /// <param name="x">横坐标 int</param>
        /// <param name="y">纵坐标</param>
        public void SetPosition(int x, int y)
        {
            _form.InvokeIfRequired(() =>
            {
                _form.Location = new Point(x, y);
            });
        }

        /// <summary>
        /// 获取窗口位置，x 横坐标，y 纵坐标
        /// </summary>
        /// <returns> (x, y) => ValueTuple&lt;int, int&gt;?</returns>
        public (int, int) GetPosition() {
            return _form.InvokeIfRequired(() =>
            {
                return (_form.Location.X, _form.Location.Y);
            });
        }

        /// <summary>
        /// 无边框
        /// </summary>
        public void CloseFormBorderStyle()
        {
            _form.InvokeIfRequired(() =>
            {
                _form.FormBorderStyle = FormBorderStyle.None;
            });
        }

        /// <summary>
        /// 有边框
        /// </summary>
        public void OpenFormBorderStyle()
        {
            _form.InvokeIfRequired(() =>
            {
                _form.FormBorderStyle = FormBorderStyle.Sizable;
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
