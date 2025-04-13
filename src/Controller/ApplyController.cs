using Smudging.src.entity;
using Smudging.src.Window;

namespace Smudging.src.Controller
{
    [ApiCustom("/apply")]
    public class ApplyController
    {

        /// <summary>
        /// 退出程序
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [ApiCustom("/exit", RequestMethod.POST)]
        public static ResponseBody Exit(int status) {
            Apply.Quit(status);
            return new ResponseBody();
        }

    }
}