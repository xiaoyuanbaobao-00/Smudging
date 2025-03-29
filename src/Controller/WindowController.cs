using Smudging.src.entity;
using Smudging.src.Server;
namespace Smudging.src.Controller
{
    [ApiCustom("/window")]
    public class WindowController {

        /// <summary>
        /// 隐藏窗口
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/hide", RequestMethod.GET)]
        public ResponseBody Hide()
        {
            HttpServer.CONTROLS?.Hide();
            return new ResponseBody(ResponseStatus.OK, null, "执行成功！");
        }

        /// <summary>
        /// 显示窗口
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/show", RequestMethod.GET)]
        public ResponseBody Show()
        {
            HttpServer.CONTROLS?.Show();
            return new ResponseBody(ResponseStatus.OK, null, "执行成功！");
        }

        /// <summary>
        /// 最大化窗口
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/maximize", RequestMethod.GET)]
        public ResponseBody Maximize()
        {
            HttpServer.CONTROLS?.Maximize();
            return new ResponseBody(ResponseStatus.OK, null, "执行成功！");
        }

        /// <summary>
        /// 最小化窗口
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/minimize", RequestMethod.GET)]
        public ResponseBody Minimize()
        {
            HttpServer.CONTROLS?.Minimize();
            return new ResponseBody(ResponseStatus.OK, null, "执行成功！");
        }

        /// <summary>
        /// 窗口化
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/normal", RequestMethod.GET)]
        public ResponseBody Normal()
        {
            HttpServer.CONTROLS?.Normal();
            return new ResponseBody(ResponseStatus.OK, null, "执行成功！");
        }

        /// <summary>
        /// 获取窗口标题
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/getitle", RequestMethod.GET)]
        public ResponseBody GetTitle()
        {
            return new ResponseBody(ResponseStatus.OK, HttpServer.CONTROLS?.GetTitle(), "执行成功！");
        }

        /// <summary>
        /// 设置窗口标题
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [ApiCustom("/setitle", RequestMethod.GET)]
        public ResponseBody SetTitle(string title)
        {
            HttpServer.CONTROLS?.SetTitle(title);
            return new ResponseBody(ResponseStatus.OK, null, "执行成功！");
        }
    }
}