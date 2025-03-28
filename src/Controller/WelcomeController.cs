using Smudging.src.entity;
namespace Smudging.src.Controller
{
    [ApiCustom("/")]
    public class WelcomeController
    {
        [ApiCustom("/welcome", RequestMethod.GET)]
        public ResponseBody Welcome()
        {
            return new ResponseBody("Welcome to Smudging!");
        }
    }
}