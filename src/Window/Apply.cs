using App;
using Smudging.src.Server;

namespace Smudging.src.Window
{
    public class Apply
    {
        private readonly Form1 form;

        public Apply()
        {
            ApplicationConfiguration.Initialize();
            form = new();
            Application.Run(form);
        }

        /// <summary>
        /// 退出结束应用
        /// </summary>
        /// <param name="status"></param>
        public static void Quit(int status = 0) {
            QueueProcessTask.Instance().Stop();
            HttpServer.Instance().Stop();
            Application.Exit();
            Environment.Exit(status);
        }
    }
}
