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
        public ResponseBody Hide()
        {
            HttpServer.CONTROLS?.Hide();
            return new ResponseBody(ResponseStatus.OK, null, "ִ�гɹ���");
        }

        /// <summary>
        /// ��ʾ����
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/show", RequestMethod.POST)]
        public ResponseBody Show()
        {
            HttpServer.CONTROLS?.Show();
            return new ResponseBody(ResponseStatus.OK, null, "ִ�гɹ���");
        }

        /// <summary>
        /// ��󻯴���
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/maximize", RequestMethod.POST)]
        public ResponseBody Maximize()
        {
            HttpServer.CONTROLS?.Maximize();
            return new ResponseBody(ResponseStatus.OK, null, "ִ�гɹ���");
        }

        /// <summary>
        /// ��С������
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/minimize", RequestMethod.POST)]
        public ResponseBody Minimize()
        {
            HttpServer.CONTROLS?.Minimize();
            return new ResponseBody(ResponseStatus.OK, null, "ִ�гɹ���");
        }

        /// <summary>
        /// ���ڻ�
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/unminimize", RequestMethod.POST)]
        public ResponseBody Unminimize()
        {
            HttpServer.CONTROLS?.Unminimize();
            return new ResponseBody(ResponseStatus.OK, null, "ִ�гɹ���");
        }

        /// <summary>
        /// ��ȡ���ڱ���
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/getitle", RequestMethod.POST)]
        public ResponseBody GetTitle()
        {
            return new ResponseBody(ResponseStatus.OK, HttpServer.CONTROLS?.GetTitle(), "ִ�гɹ���");
        }

        /// <summary>
        /// ���ô��ڱ���
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [ApiCustom("/title", RequestMethod.POST)]
        public ResponseBody SetTitle(string title)
        {
            HttpServer.CONTROLS?.SetTitle(title);
            return new ResponseBody(ResponseStatus.OK, null, "ִ�гɹ���");
        }

        /// <summary>
        /// �жϴ����Ƿ�����
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/visible", RequestMethod.POST)]
        public ResponseBody IsVisible() {
            return new ResponseBody(ResponseStatus.OK, HttpServer.CONTROLS?.IsVisible(), "ִ�гɹ���");
        }

        /// <summary>
        /// �ö�����
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/topmost", RequestMethod.POST)]
        public ResponseBody TopMost() {
            HttpServer.CONTROLS?.TopMost();
            return new ResponseBody(ResponseStatus.OK, null, "ִ�гɹ���");
        }

        /// <summary>
        /// ȡ���ö�����
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/topmostcancel", RequestMethod.POST)]
        public ResponseBody TopMostCancel() {
            HttpServer.CONTROLS?.TopMostCancel();
            return new ResponseBody(ResponseStatus.OK, null, "ִ�гɹ���");
        }

        /// <summary>
        /// �����
        /// </summary>
        /// <returns></returns>a
        [ApiCustom("/activate", RequestMethod.POST)]
        public ResponseBody Activate() {
            HttpServer.CONTROLS?.Activate();
            return new ResponseBody(ResponseStatus.OK, null, "ִ�гɹ���");
        }

        /// <summary>
        /// �۽�����
        /// </summary>
        /// <returns></returns>a
        [ApiCustom("/setfocus", RequestMethod.POST)]
        public ResponseBody SetFocus() {
            HttpServer.CONTROLS?.SetFocus();
            return new ResponseBody(ResponseStatus.OK, null, "ִ�гɹ���");
        }

        /// <summary>
        /// ���ô��ڴ�С
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        [ApiCustom("/setsize", RequestMethod.POST)]
        public ResponseBody SetWindowSize(int width, int height) {
            HttpServer.CONTROLS?.SetWindowSize(width, height);
            return new ResponseBody(ResponseStatus.OK, null, "ִ�гɹ���");
        }
        
        /// <summary>
        /// ��ȡ���ڴ�С
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/size", RequestMethod.POST)]
        public ResponseBody GetWindowSize() {
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
    }
}