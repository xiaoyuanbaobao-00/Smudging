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
        [ApiCustom("/hide", RequestMethod.POST)]
        public ResponseBody Hide()
        {
            HttpServer.CONTROLS?.Hide();
            return new ResponseBody(ResponseStatus.OK, null, "执行成功！");
        }

        /// <summary>
        /// 显示窗口
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/show", RequestMethod.POST)]
        public ResponseBody Show()
        {
            HttpServer.CONTROLS?.Show();
            return new ResponseBody(ResponseStatus.OK, null, "执行成功！");
        }

        /// <summary>
        /// 最大化窗口
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/maximize", RequestMethod.POST)]
        public ResponseBody Maximize()
        {
            HttpServer.CONTROLS?.Maximize();
            return new ResponseBody(ResponseStatus.OK, null, "执行成功！");
        }

        /// <summary>
        /// 最小化窗口
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/minimize", RequestMethod.POST)]
        public ResponseBody Minimize()
        {
            HttpServer.CONTROLS?.Minimize();
            return new ResponseBody(ResponseStatus.OK, null, "执行成功！");
        }

        /// <summary>
        /// 窗口化
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/unminimize", RequestMethod.POST)]
        public ResponseBody Unminimize()
        {
            HttpServer.CONTROLS?.Unminimize();
            return new ResponseBody(ResponseStatus.OK, null, "执行成功！");
        }

        /// <summary>
        /// 获取窗口标题
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/getitle", RequestMethod.POST)]
        public ResponseBody GetTitle()
        {
            return new ResponseBody(ResponseStatus.OK, HttpServer.CONTROLS?.GetTitle(), "执行成功！");
        }

        /// <summary>
        /// 设置窗口标题
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [ApiCustom("/title", RequestMethod.POST)]
        public ResponseBody SetTitle(string title)
        {
            HttpServer.CONTROLS?.SetTitle(title);
            return new ResponseBody(ResponseStatus.OK, null, "执行成功！");
        }

        /// <summary>
        /// 判断窗口是否隐藏
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/visible", RequestMethod.POST)]
        public ResponseBody IsVisible() {
            return new ResponseBody(ResponseStatus.OK, HttpServer.CONTROLS?.IsVisible(), "执行成功！");
        }

        /// <summary>
        /// 置顶窗口
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/topmost", RequestMethod.POST)]
        public ResponseBody TopMost() {
            HttpServer.CONTROLS?.TopMost();
            return new ResponseBody(ResponseStatus.OK, null, "执行成功！");
        }

        /// <summary>
        /// 取消置顶窗口
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/topmostcancel", RequestMethod.POST)]
        public ResponseBody TopMostCancel() {
            HttpServer.CONTROLS?.TopMostCancel();
            return new ResponseBody(ResponseStatus.OK, null, "执行成功！");
        }

        /// <summary>
        /// 激活窗口
        /// </summary>
        /// <returns></returns>a
        [ApiCustom("/activate", RequestMethod.POST)]
        public ResponseBody Activate() {
            HttpServer.CONTROLS?.Activate();
            return new ResponseBody(ResponseStatus.OK, null, "执行成功！");
        }

        /// <summary>
        /// 聚焦窗口
        /// </summary>
        /// <returns></returns>a
        [ApiCustom("/setfocus", RequestMethod.POST)]
        public ResponseBody SetFocus() {
            HttpServer.CONTROLS?.SetFocus();
            return new ResponseBody(ResponseStatus.OK, null, "执行成功！");
        }

        /// <summary>
        /// 设置窗口大小
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        [ApiCustom("/setsize", RequestMethod.POST)]
        public ResponseBody SetWindowSize(int width, int height) {
            HttpServer.CONTROLS?.SetWindowSize(width, height);
            return new ResponseBody(ResponseStatus.OK, null, "执行成功！");
        }
        
        /// <summary>
        /// 获取窗口大小
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/size", RequestMethod.POST)]
        public ResponseBody GetWindowSize() {
            if(HttpServer.CONTROLS == null) {
                return new ResponseBody(ResponseStatus.ERROR, null, "CONTROLS未初始化！");
            }
            ValueTuple<int, int>? size = HttpServer.CONTROLS.GetWindowSize(); // 修改返回类型为可空元组
            if (size == null)
            {
                return new ResponseBody(ResponseStatus.ERROR, null, "无法获取窗口大小！");
            }

            return new ResponseBody(ResponseStatus.OK, $"{{ width: {size.Value.Item1}, height: {size.Value.Item2} }}", "执行成功！");
        }
    }
}