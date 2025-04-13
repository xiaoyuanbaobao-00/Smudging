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
        public static ResponseBody Hide()
        {
            HttpServer.CONTROLS?.Hide();
            return new ResponseBody();
        }

        /// <summary>
        /// 显示窗口
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/show", RequestMethod.POST)]
        public static ResponseBody Show()
        {
            HttpServer.CONTROLS?.Show();
            return new ResponseBody();
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/close", RequestMethod.POST)]
        public static ResponseBody Close() {
            HttpServer.CONTROLS?.Close();
            return new ResponseBody();
        }

        /// <summary>
        /// 最大化窗口
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/maximize", RequestMethod.POST)]
        public static ResponseBody Maximize()
        {
            HttpServer.CONTROLS?.Maximize();
            return new ResponseBody();
        }

        /// <summary>
        /// 最小化窗口
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/minimize", RequestMethod.POST)]
        public static ResponseBody Minimize()
        {
            HttpServer.CONTROLS?.Minimize();
            return new ResponseBody();
        }

        /// <summary>
        /// 窗口化
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/unminimize", RequestMethod.POST)]
        public static ResponseBody Unminimize()
        {
            HttpServer.CONTROLS?.Unminimize();
            return new ResponseBody();
        }

        /// <summary>
        /// 获取窗口标题
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/getitle", RequestMethod.POST)]
        public static ResponseBody GetTitle()
        {
            return new ResponseBody(ResponseStatus.OK, HttpServer.CONTROLS?.GetTitle(), "执行成功！");
        }

        /// <summary>
        /// 设置窗口标题
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [ApiCustom("/title", RequestMethod.POST)]
        public static ResponseBody SetTitle(string title)
        {
            HttpServer.CONTROLS?.SetTitle(title);
            return new ResponseBody();
        }

        /// <summary>
        /// 判断窗口是否隐藏
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/visible", RequestMethod.POST)]
        public static ResponseBody IsVisible() {
            return new ResponseBody(ResponseStatus.OK, HttpServer.CONTROLS?.IsVisible(), "执行成功！");
        }

        /// <summary>
        /// 置顶窗口
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/topmost", RequestMethod.POST)]
        public static ResponseBody TopMost() {
            HttpServer.CONTROLS?.TopMost();
            return new ResponseBody();
        }

        /// <summary>
        /// 取消置顶窗口
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/topmostcancel", RequestMethod.POST)]
        public static ResponseBody TopMostCancel() {
            HttpServer.CONTROLS?.TopMostCancel();
            return new ResponseBody();
        }

        /// <summary>
        /// 激活窗口
        /// </summary>
        /// <returns></returns>a
        [ApiCustom("/activate", RequestMethod.POST)]
        public static ResponseBody Activate() {
            HttpServer.CONTROLS?.Activate();
            return new ResponseBody();
        }

        /// <summary>
        /// 聚焦窗口
        /// </summary>
        /// <returns></returns>a
        [ApiCustom("/setfocus", RequestMethod.POST)]
        public static ResponseBody SetFocus() {
            HttpServer.CONTROLS?.SetFocus();
            return new ResponseBody();
        }

        /// <summary>
        /// 设置窗口大小
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        [ApiCustom("/setsize", RequestMethod.POST)]
        public static ResponseBody SetWindowSize(int width, int height) {
            HttpServer.CONTROLS?.SetWindowSize(width, height);
            return new ResponseBody();
        }
        
        /// <summary>
        /// 获取窗口大小
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/size", RequestMethod.POST)]
        public static ResponseBody GetWindowSize() {
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

        /// <summary>
        /// 获取窗口位置
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/position", RequestMethod.POST)]
        public static ResponseBody GetPosition() {
            if(HttpServer.CONTROLS == null) {
                return new ResponseBody(ResponseStatus.ERROR, null, "CONTROLS未初始化！");
            }
            ValueTuple<int, int>? position = HttpServer.CONTROLS.GetPosition();
            if (position == null)
            {
                return new ResponseBody(ResponseStatus.ERROR, null, "无法获取窗口位置！");
            }
            return new ResponseBody(ResponseStatus.OK, $"{{ x: {position.Value.Item1}, y: {position.Value.Item2} }}", "执行成功！");
        }

        /// <summary>
        /// 设置窗口位置
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [ApiCustom("/position", RequestMethod.POST)]
        public static ResponseBody SetPosition(int x, int y) {
            HttpServer.CONTROLS?.SetPosition(x, y);
            return new ResponseBody();
        }

        /// <summary>
        /// 无边框
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/frameless", RequestMethod.POST)]
        public static ResponseBody Frameless() {
            HttpServer.CONTROLS?.CloseFormBorderStyle();
            return new ResponseBody();
        }

        /// <summary>
        /// 有边框
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/bordered", RequestMethod.POST)]
        public static ResponseBody Bordered() {
            HttpServer.CONTROLS?.OpenFormBorderStyle();
            return new ResponseBody();
        }

        /// <summary>
        /// 无边框最大化
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/framelessmaximize", RequestMethod.POST)]
        public static ResponseBody FramelessMaximize() {
            HttpServer.CONTROLS?.CloseFormBorderStyle();
            HttpServer.CONTROLS?.Maximize();
            HttpServer.CONTROLS?.SetFocus();
            return new ResponseBody();
        }

    }
}