namespace Uno_Game
{
    public class UnoController
    {
        private UnoModel model;
        private UnoView view;

        public UnoController(UnoModel model, UnoView view)
        {
            this.model = model;
            this.view = view;
        }

        public void Run()
        {
            view.StartGame();
        }
    }
}