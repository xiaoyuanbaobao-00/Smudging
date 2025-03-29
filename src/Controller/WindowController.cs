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
        [ApiCustom("/normal", RequestMethod.GET)]
        public ResponseBody Normal()
        {
            HttpServer.CONTROLS?.Normal();
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
    }
}