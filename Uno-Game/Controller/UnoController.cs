namespace Uno_Game
{
    public class UnoController
    {
        private UnoView view;
        private UnoModel model;
        static Players player = new Players();
        static List<string> center = new List<string>();
        static List<Players> players = new List<Players>();
        public UnoController(UnoModel model, UnoView view)
        {
            this.model = model;
            this.view = view;
        }

        public void Run()
        {
            view.Start();
            view.ChoosePlayerCount();
            view.ChoosePlayerNames();
            view.RuleQuery();
            player.StartingPlayer = model.ChooseStartingPlayer();
            
            List<string> colors = new List<string> { "Red", "Green", "Blue", "Yellow" };
            List<string> values = Enumerable.Range(0, 10).Select(i => i.ToString())
                .Concat(new string[] { "Skip", "+2", "Reverse" }).ToList();
            List<string> specialCards = new List<string> { "Wild ", "Draw 4" };
            player.Deck = model.GenerateDeck(colors, values, specialCards);
            
            model.ShuffleDeck();

            foreach (Players player in players)
            {
                player.Hand = model.DealCards( 7);
            }

            string firstCard = model.PlaceFirstCardInCenter(center);
            
        }
    }
}