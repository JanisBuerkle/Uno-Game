namespace Uno_Game
{
    public class UnoModel
    {
        static Random random = new Random();
        static Players player = new Players();
        public int ChooseStartingPlayer()
        {
            return random.Next(0, player.CountOfPlayers);
        }
        public List<string> GenerateDeck(List<string> colors, List<string> values, List<string> specialCards)
        {

            foreach (string color in colors)
            {
                foreach (string value in values)
                {
                    player.Deck.Add(color + " " + value);
                    if (value != "0")
                    {
                        player.Deck.Add(color + " " + value);
                    }
                }
            }
            foreach (string specialCard in specialCards)
            {
                for (; player.Player < player.CountOfPlayers; player.Player++)
                {
                    player.Deck.Add(specialCard);
                }
            }
            return player.Deck;
        }
        public void ShuffleDeck()
        {
            int number = player.Deck.Count;
            while (number > 1)
            {
                number--;
                int card = random.Next(number + 1);
                string value = player.Deck[card];
                player.Deck[card] = player.Deck[number];
                player.Deck[number] = value;
            }
        }
        public List<string> DealCards(int handSize)
        {
            List<string> hand = new List<string>();
            for (player.Player = 0; player.Player < handSize; player.Player++)
            {
                string card = player.Deck.First();
                player.Deck.RemoveAt(0);
                hand.Add(card);
            }
            return hand;
        }
        public void PrintHand(List<string> hand)
        {
            double nummer = -1;
            foreach (string card in hand)
            {
                ConsoleColor consoleColor;
                string[] cardColor = card.Split(' ');

                if (Enum.TryParse(cardColor[0], true, out consoleColor))
                {
                    nummer++;
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("Karte " + nummer + ": ");
                    Console.ForegroundColor = consoleColor;
                    Console.WriteLine(card);
                    Console.ResetColor();
                }
                else if (card.Contains("Wild") || card.Contains("Draw 4"))
                {
                    nummer++;
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("Karte " + nummer + ": ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(card);
                    Console.ResetColor();
                }
            }
        }
        public string PlaceFirstCardInCenter(List<string> center)
        {
            int randomCard = random.Next(player.Deck.Count);
            string selectedCard = player.Deck[randomCard];
            player.Deck.RemoveAt(randomCard);
            center.Add(selectedCard);

            return selectedCard;
        }
        public string DrawCard(List<string> center)
        {
            if (player.Deck.Count == 0)
            {
                if (center.Count > 1)
                {
                    ShuffleDeck();
                    Console.WriteLine("Stapel ist Leer wird neu gemischt...");
                    Thread.Sleep(2000);
                    Console.WriteLine(center[0]);
                
                    int number = center.Count;
                    string first = center[0];
                    while (number > 1)
                    {
                        number--;
                        int carde = random.Next(number + 1);
                        string value = center[carde];
                        center[carde] = center[number];
                        center[number] = value;
                    }
                    int i = 1;
                    while (i < center.Count)
                    {
                        player.Deck.Add(center[i]);
                        i++;
                    }
                    center.Clear();
                    center.Add(first);
                
                    i = 0;
                    while (i < center.Count)
                    {
                        Console.WriteLine(center[i]);
                        i++;
                    }
                }
                else
                {
                    Console.WriteLine("Stapel ist Leer und es liegen keine Karten mehr die man mischen kann.");
                    Console.WriteLine("Ihr habt alle Karten auf der Hand spielt mit diesen!");
                    string testen = "";
                    return testen;
                }
            }
            var card = player.Deck.First();
            player.Deck.RemoveAt(0);
            return card;
        }
        public bool IsCardPlayable(string selectedCard, List<string> center)
        {
            if (center.Count == 0)
            {
                return true;
            }

            string[] selectedCardParts = selectedCard.Split(' ');
            if (selectedCardParts.Length != 2)
            {
                selectedCardParts = selectedCard.Split(' ');
            }

            string selectedColor = selectedCardParts[0];
            string selectedValue = selectedCardParts[1];

            string centerCard = center.LastOrDefault();
            if (centerCard == null)
            {
                return false;
            }

            string[] centerCardParts = centerCard.Split(' ');
            if (centerCardParts.Length != 2)
            {
                return false;
            }

            string centerColor = centerCardParts[0];
            string centerValue = centerCardParts[1];

            if (selectedCard.Contains("+2"))
            {
                return true;
            }

            if (selectedCard.Contains("Wild") || selectedCard.Contains("Draw 4"))
            {
                return true;
            }

            if (selectedColor == centerColor || selectedValue == centerValue)
            {
                return true;
            }
            return false;
        }
        public bool IsTwoPlus(string card)
        {
            return card.Contains("+2");
        }

        public bool IsSkip(string card)
        {
            return card.Contains("Skip");
        }
        public bool IsReverse(string card)
        {
            return card.Contains("Reverse");
        }
        public bool IsWildcard(string card)
        {
            return card.Contains("Wild") || card.Contains("Draw 4");
        }
        public bool IsValidColor(string color)
        {
            List<string> validColors = new List<string> { "red", "green", "blue", "yellow" };
            return validColors.Contains(color.ToLower());
        }
    }
}