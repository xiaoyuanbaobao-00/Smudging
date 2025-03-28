using Smudging.src.entity;
using Smudging.src.Server;
namespace Smudging.src.Controller
{
    [ApiCustom("/custom")]
    public class CustomController {

        [ApiCustom("/hide", RequestMethod.GET)]
        public ResponseBody hide()
        {
            HttpServer.CONTROLS?.Hide();
            return new ResponseBody(ResponseStatus.OK, null, "ִ�гɹ���");
        }

        [ApiCustom("/show", RequestMethod.GET)]
        public ResponseBody show()
        {
            HttpServer.CONTROLS?.Show();
            return new ResponseBody(ResponseStatus.OK, null, "ִ�гɹ���");
        }

        [ApiCustom("/maximize", RequestMethod.GET)]
        public ResponseBody maximize()
        {
            HttpServer.CONTROLS?.Maximize();
            return new ResponseBody(ResponseStatus.OK, null, "ִ�гɹ���");
        }

        [ApiCustom("/minimize", RequestMethod.GET)]
        public ResponseBody minimize()
        {
            HttpServer.CONTROLS?.Minimize();
            return new ResponseBody(ResponseStatus.OK, null, "ִ�гɹ���");
        }

        [ApiCustom("/normal", RequestMethod.GET)]
        public ResponseBody normal()
        {
            HttpServer.CONTROLS?.Normal();
            return new ResponseBody(ResponseStatus.OK, null, "ִ�гɹ���");
        }

        [ApiCustom("/gettitle", RequestMethod.GET)]
        public ResponseBody getTitle()
        {
            return new ResponseBody(ResponseStatus.OK, HttpServer.CONTROLS?.GetTitle(), "ִ�гɹ���");
        }

        [ApiCustom("/settitle", RequestMethod.GET)]
        public ResponseBody setTitle(string title)
        {
            HttpServer.CONTROLS?.SetTitle(title);
            return new ResponseBody(ResponseStatus.OK, null, "ִ�гɹ���");
        }

        [ApiCustom("/setwebsource", RequestMethod.GET)]
        public ResponseBody setUrl(string url)
        {
            HttpServer.CONTROLS?.SetWebSource(url);
            return new ResponseBody(ResponseStatus.OK, null, "ִ�гɹ���");
        }

    }
}