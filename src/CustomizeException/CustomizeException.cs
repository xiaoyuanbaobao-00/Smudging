namespace Smudging.src.CustomizeException
{
    public class MyException
    {
        public static string WindowTitle = "App";
        public static void ErrorException(string message, Exception? innerException = null)
        {
            MessageBox.Show(message + "：\n" + innerException?.Message, WindowTitle + "错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(1);
        }

        public static void WarningException(string message)
        {
            MessageBox.Show(message, WindowTitle + "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // 消息弹窗
        public static void Info(string message, string? title = "")
        {
            MessageBox.Show(message, WindowTitle + title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}