namespace Smudging.src.CustomizeException
{
    /// <summary>
    /// 自定义异常类
    /// </summary>
    public class MyException
    {
        public static string WindowTitle = "App";

        /// <summary>
        /// 错误提示，弹窗，退出程序
        /// </summary>
        public static void ErrorException(string message, Exception? innerException = null)
        {
            MessageBox.Show(message + "：\n" + innerException?.Message, WindowTitle + "错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(1);
        }

        /// <summary>
        /// 警告提示，弹窗
        /// </summary>
        public static void WarningException(string message)
        {
            MessageBox.Show(message, WindowTitle + "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 信息提示，弹窗
        /// </summary>
        public static void Info(string message, string? title = "")
        {
            MessageBox.Show(message, WindowTitle + title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}