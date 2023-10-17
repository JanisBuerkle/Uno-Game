namespace Uno_Game
{
    public class UnoController
    {
        private UnoView view;
        private UnoModel model;

        public UnoController(UnoModel model, UnoView view)
        {
            this.model = model;
            this.view = view;
        }

        public void Run()
        {
            view.Start();
            model.player.CountOfPlayers = view.ChoosePlayerCount();
            model.player.CountOfPlayers = view.ChoosePlayerNames();
            view.RuleQuery();
            model.player.StartingPlayer = model.ChooseStartingPlayer();
            
            List<string> colors = new List<string> { "Red", "Green", "Blue", "Yellow" };
            List<string> values = Enumerable.Range(0, 10).Select(i => i.ToString()).Concat(new string[] { "Skip", "+2", "Reverse" }).ToList();
            List<string> specialCards = new List<string> { "Wild ", "Draw 4" };
            
            model.player.Deck = model.GenerateDeck(colors, values, specialCards);
            model.ShuffleDeck(); 
            
            foreach (Players player in model.players)
            {
                player.Hand = model.DealCards(7);
            }

            model.player.Center = model.PlaceFirstCardInCenter();
            // Console.WriteLine(model.player.Center.Count);
            
            while (true)
            {
                if (model.player.Richtung == 0)
                {
                    model.player.Player = model.player.StartingPlayer;
                    if (model.player.ReverseCardPlayed == 1)
                    {
                        model.player.Player = model.player.NextPlayer;
                        model.player.ReverseCardPlayed = 0;
                    }
                    for (; model.player.Player <= model.player.CountOfPlayers - 1; model.player.Player++)
                    {
                        view.PlayerDisplay();
                        view.Center();
                        view.CardDisplay();
                        
                        model.player.validCardPlayed = false;
                        while (!model.player.validCardPlayed)
                        {
                            view.GameTurn();
                            view.Play();
                            
                        }
                    }
                    model.player.StartingPlayer = 0;
                }
                else if (model.player.Richtung == 1)
                {
                    model.player.Player = model.player.StartingPlayer;
                    if (model.player.ReverseCardPlayed == 1)
                    {
                        model.player.Player = model.player.NextPlayer;
                        model.player.ReverseCardPlayed = 0;
                    }
                    for (model.player.Player = model.player.Player; model.player.Player >= 0; model.player.Player--)
                    {
                        view.PlayerDisplay();
                        view.Center();
                        view.CardDisplay();
                        
                        model.player.validCardPlayed = false;
                        while (!model.player.validCardPlayed)
                        {
                            view.GameTurn();
                            view.Play();
                            
                        }
                    }
                    model.player.StartingPlayer = model.player.CountOfPlayers - 1;
                }
            }
        }
    }
}