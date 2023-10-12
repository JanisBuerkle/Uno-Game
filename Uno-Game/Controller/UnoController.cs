using System.Data.Common;

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
            view.ChoosePlayerCount();
            view.ChoosePlayerNames();
            view.RuleQuery();
            model.player.StartingPlayer = model.ChooseStartingPlayer();
            
            List<string> colors = new List<string> { "Red", "Green", "Blue", "Yellow" };
            List<string> values = Enumerable.Range(0, 10).Select(i => i.ToString())
                .Concat(new string[] { "Skip", "+2", "Reverse" }).ToList();
            List<string> specialCards = new List<string> { "Wild ", "Draw 4" };

            model.player.CountOfPlayers = model.players.Count;
            model.player.Deck = model.GenerateDeck(colors, values, specialCards);
            model.ShuffleDeck(); 
            
            foreach (Players player in model.players)
            {
                player.Hand = model.DealCards(7);
            }

            string firstCard = model.PlaceFirstCardInCenter(model.center);
            Console.WriteLine(firstCard);
            
            while (true)
            {
                if (model.player.Reset == 0)
                {
                    model.player.Player = model.player.StartingPlayer;
                    if (model.player.ReverseCardPlayed == 1)
                    {
                        model.player.Player = model.player.NextPlayer;
                        model.player.ReverseCardPlayed = 0;
                    }
                    for (model.player.Player = model.player.Player; model.player.Player <= model.player.CountOfPlayers - 1; model.player.Player++)
                    {
                        view.PlayerDisplay();
                        view.Game();
                    }
                    model.player.StartingPlayer = 0;
                }
                else if (model.player.Reset == 1)
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
                        view.Game();
                    }
                    model.player.StartingPlayer = model.player.CountOfPlayers - 1;
                }
            }
        }
    }
}