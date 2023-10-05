namespace Uno_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            UnoModel model = new UnoModel();
            UnoView view = new UnoView();
            UnoController controller = new UnoController(model, view);

            controller.Run();
        }
    }
}