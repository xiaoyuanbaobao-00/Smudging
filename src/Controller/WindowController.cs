using Smudging.src.entity;
using Smudging.src.Server;
namespace Smudging.src.Controller
{
    [ApiCustom("/window")]
    public class WindowController {

        /// <summary>
        /// ���ش���
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/hide", RequestMethod.POST)]
        public static ResponseBody Hide()
        {
            HttpServer.CONTROLS?.Hide();
            return new ResponseBody();
        }

        /// <summary>
        /// ��ʾ����
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/show", RequestMethod.POST)]
        public static ResponseBody Show()
        {
            HttpServer.CONTROLS?.Show();
            return new ResponseBody();
        }

        /// <summary>
        /// �رմ���
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/close", RequestMethod.POST)]
        public static ResponseBody Close() {
            HttpServer.CONTROLS?.Close();
            return new ResponseBody();
        }

        /// <summary>
        /// ��󻯴���
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/maximize", RequestMethod.POST)]
        public static ResponseBody Maximize()
        {
            HttpServer.CONTROLS?.Maximize();
            return new ResponseBody();
        }

        /// <summary>
        /// ��С������
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/minimize", RequestMethod.POST)]
        public static ResponseBody Minimize()
        {
            HttpServer.CONTROLS?.Minimize();
            return new ResponseBody();
        }

        /// <summary>
        /// ���ڻ�
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/unminimize", RequestMethod.POST)]
        public static ResponseBody Unminimize()
        {
            HttpServer.CONTROLS?.Unminimize();
            return new ResponseBody();
        }

        /// <summary>
        /// ��ȡ���ڱ���
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/getitle", RequestMethod.POST)]
        public static ResponseBody GetTitle()
        {
            return new ResponseBody(ResponseStatus.OK, HttpServer.CONTROLS?.GetTitle(), "ִ�гɹ���");
        }

        /// <summary>
        /// ���ô��ڱ���
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
        /// �жϴ����Ƿ�����
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/visible", RequestMethod.POST)]
        public static ResponseBody IsVisible() {
            return new ResponseBody(ResponseStatus.OK, HttpServer.CONTROLS?.IsVisible(), "ִ�гɹ���");
        }

        /// <summary>
        /// �ö�����
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/topmost", RequestMethod.POST)]
        public static ResponseBody TopMost() {
            HttpServer.CONTROLS?.TopMost();
            return new ResponseBody();
        }

        /// <summary>
        /// ȡ���ö�����
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/topmostcancel", RequestMethod.POST)]
        public static ResponseBody TopMostCancel() {
            HttpServer.CONTROLS?.TopMostCancel();
            return new ResponseBody();
        }

        /// <summary>
        /// �����
        /// </summary>
        /// <returns></returns>a
        [ApiCustom("/activate", RequestMethod.POST)]
        public static ResponseBody Activate() {
            HttpServer.CONTROLS?.Activate();
            return new ResponseBody();
        }

        /// <summary>
        /// �۽�����
        /// </summary>
        /// <returns></returns>a
        [ApiCustom("/setfocus", RequestMethod.POST)]
        public static ResponseBody SetFocus() {
            HttpServer.CONTROLS?.SetFocus();
            return new ResponseBody();
        }

        /// <summary>
        /// ���ô��ڴ�С
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
        /// ��ȡ���ڴ�С
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/size", RequestMethod.POST)]
        public static ResponseBody GetWindowSize() {
            if(HttpServer.CONTROLS == null) {
                return new ResponseBody(ResponseStatus.ERROR, null, "CONTROLSδ��ʼ����");
            }
            ValueTuple<int, int>? size = HttpServer.CONTROLS.GetWindowSize(); // �޸ķ�������Ϊ�ɿ�Ԫ��
            if (size == null)
            {
                return new ResponseBody(ResponseStatus.ERROR, null, "�޷���ȡ���ڴ�С��");
            }

            return new ResponseBody(ResponseStatus.OK, $"{{ width: {size.Value.Item1}, height: {size.Value.Item2} }}", "ִ�гɹ���");
        }

        /// <summary>
        /// ��ȡ����λ��
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/position", RequestMethod.POST)]
        public static ResponseBody GetPosition() {
            if(HttpServer.CONTROLS == null) {
                return new ResponseBody(ResponseStatus.ERROR, null, "CONTROLSδ��ʼ����");
            }
            ValueTuple<int, int>? position = HttpServer.CONTROLS.GetPosition();
            if (position == null)
            {
                return new ResponseBody(ResponseStatus.ERROR, null, "�޷���ȡ����λ�ã�");
            }
            return new ResponseBody(ResponseStatus.OK, $"{{ x: {position.Value.Item1}, y: {position.Value.Item2} }}", "ִ�гɹ���");
        }

        /// <summary>
        /// ���ô���λ��
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
        /// �ޱ߿�
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/frameless", RequestMethod.POST)]
        public static ResponseBody Frameless() {
            HttpServer.CONTROLS?.CloseFormBorderStyle();
            return new ResponseBody();
        }

        /// <summary>
        /// �б߿�
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/bordered", RequestMethod.POST)]
        public static ResponseBody Bordered() {
            HttpServer.CONTROLS?.OpenFormBorderStyle();
            return new ResponseBody();
        }

        /// <summary>
        /// �ޱ߿����
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