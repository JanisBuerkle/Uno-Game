namespace Uno_Game
{
    class Program
    {
        static void Main()
        {
            List<Players> players = new List<Players>{};
            UnoModel model = new UnoModel();
            UnoView view = new UnoView(model);
            UnoController controller = new UnoController(model, view);

            controller.Run();
        }
    }
}