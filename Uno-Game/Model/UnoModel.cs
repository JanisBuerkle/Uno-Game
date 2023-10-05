namespace Uno_Game
{
    public class UnoModel
    {
        static Random random = new Random();
        static Players player = new Players();
        public int ChooseStartingPlayer()
        {
            return random.Next(0, 4);
        }
        
        public List<string> GenerateDeck(List<string> colors, List<string> values, List<string> specialCards)
        {
            List<string> deck = new List<string>();

            foreach (string color in colors)
            {
                foreach (string value in values)
                {
                    deck.Add(color + " " + value);
                    if (value != "0")
                    {
                        deck.Add(color + " " + value);
                    }
                }
            }

            foreach (string specialCard in specialCards)
            {
                for (; player.I < 4; player.I++)
                {
                    deck.Add(specialCard);
                }
            }

            return deck;
        }
        
        public void ShuffleDeck(List<string> deck)
        {
            int number = deck.Count;
            while (number > 1)
            {
                number--;
                int card = random.Next(number + 1);
                string value = deck[card];
                deck[card] = deck[number];
                deck[number] = value;
            }
        }
        public List<string> DealCards(List<string> deck, int handSize)
        {
            List<string> hand = new List<string>();
            for (player.I = 0; player.I < handSize; player.I++)
            {
                string card = deck.First();
                deck.RemoveAt(0);
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
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ALARM!!! ALARM!!");
                }
            }
        }

        public string PlaceFirstCardInCenter(List<string> deck)
        {
            int randomCard = random.Next(deck.Count);
            string selectedCard = deck[randomCard];
            deck.RemoveAt(randomCard);
            player.Center.Add(selectedCard);

            return selectedCard;
        }
        public List<string> Shuffle()
        {
            ShuffleDeck(player.Center);
            return player.Center;
        }
        public string DrawCard(List<string> deck)
        {
            if (deck.Count == 0)
            {
                Console.WriteLine("LEEEEEEEEEEEEEEEEEEEEEEEEEEEERRRRRRRRRRRRRRRRRRR");
                Shuffle();
            }

            deck = Shuffle();
            var card = deck.First();
            deck.RemoveAt(0);
            return card;
        }

        public bool IsCardPlayable(string selectedCard)
        {
            if (player.Center.Count == 0)
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

            string centerCard = player.Center.LastOrDefault();
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