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
        [ApiCustom("/hide", RequestMethod.GET)]
        public ResponseBody Hide()
        {
            HttpServer.CONTROLS?.Hide();
            return new ResponseBody(ResponseStatus.OK, null, "ִ�гɹ���");
        }

        /// <summary>
        /// ��ʾ����
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/show", RequestMethod.GET)]
        public ResponseBody Show()
        {
            HttpServer.CONTROLS?.Show();
            return new ResponseBody(ResponseStatus.OK, null, "ִ�гɹ���");
        }

        /// <summary>
        /// ��󻯴���
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/maximize", RequestMethod.GET)]
        public ResponseBody Maximize()
        {
            HttpServer.CONTROLS?.Maximize();
            return new ResponseBody(ResponseStatus.OK, null, "ִ�гɹ���");
        }

        /// <summary>
        /// ��С������
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/minimize", RequestMethod.GET)]
        public ResponseBody Minimize()
        {
            HttpServer.CONTROLS?.Minimize();
            return new ResponseBody(ResponseStatus.OK, null, "ִ�гɹ���");
        }

        /// <summary>
        /// ���ڻ�
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/unminimize", RequestMethod.GET)]
        public ResponseBody Unminimize()
        {
            HttpServer.CONTROLS?.Unminimize();
            return new ResponseBody(ResponseStatus.OK, null, "ִ�гɹ���");
        }

        /// <summary>
        /// ��ȡ���ڱ���
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/getitle", RequestMethod.GET)]
        public ResponseBody GetTitle()
        {
            return new ResponseBody(ResponseStatus.OK, HttpServer.CONTROLS?.GetTitle(), "ִ�гɹ���");
        }

        /// <summary>
        /// ���ô��ڱ���
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [ApiCustom("/setitle", RequestMethod.GET)]
        public ResponseBody SetTitle(string title)
        {
            HttpServer.CONTROLS?.SetTitle(title);
            return new ResponseBody(ResponseStatus.OK, null, "ִ�гɹ���");
        }

        /// <summary>
        /// �жϴ����Ƿ�����
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/visible", RequestMethod.GET)]
        public ResponseBody IsVisible() {
            return new ResponseBody(ResponseStatus.OK, HttpServer.CONTROLS?.IsVisible(), "ִ�гɹ���");
        }

        /// <summary>
        /// �ö�����
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/topmost", RequestMethod.GET)]
        public ResponseBody TopMost() {
            HttpServer.CONTROLS?.TopMost();
            return new ResponseBody(ResponseStatus.OK, null, "ִ�гɹ���");
        }

        /// <summary>
        /// ȡ���ö�����
        /// </summary>
        /// <returns></returns>
        [ApiCustom("/topmostcancel", RequestMethod.GET)]
        public ResponseBody TopMostCancel() {
            HttpServer.CONTROLS?.TopMostCancel();
            return new ResponseBody(ResponseStatus.OK, null, "ִ�гɹ���");
        }

        /// <summary>
        /// �����
        /// </summary>
        /// <returns></returns>a
        [ApiCustom("/activate", RequestMethod.GET)]
        public ResponseBody Activate() {
            HttpServer.CONTROLS?.Activate();
            return new ResponseBody(ResponseStatus.OK, null, "ִ�гɹ���");
        }

        /// <summary>
        /// �۽�����
        /// </summary>
        /// <returns></returns>a
        [ApiCustom("/setfocus", RequestMethod.GET)]
        public ResponseBody SetFocus() {
            HttpServer.CONTROLS?.SetFocus();
            return new ResponseBody(ResponseStatus.OK, null, "ִ�гɹ���");
        }
        
    }
}