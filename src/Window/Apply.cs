using App;

namespace Smudging.src.Window
{
    public class Apply
    {

        private readonly Form1 form;

        public Apply()
        {
            ApplicationConfiguration.Initialize();
            form = new();
            Application.Run(form);
        }

    }
}
