class Program
{
    static Random random = new Random();
    static List<string> center = new List<string>();
    static View view = new View();
    static Players player = new Players();


    static List<Players> players = new List<Players>()
    {
        new Players(),
        new Players(),
        new Players(),
        new Players(),
    };

    static void Main(string[] args)
    {
        Console.WriteLine("Willkommen bei UNO!");
        Console.WriteLine("Dieses Spiel ist für 4 Spieler.");

        for (int playerNumb = 0; playerNumb < 4; playerNumb++)
        {
            Console.WriteLine("Gebt den Namen ein:");
            string player = Console.ReadLine();
            players[playerNumb].Name = player;
        }

        Console.WriteLine("Da wir dies nun haben fangen wir doch direkt an!");
        Console.WriteLine("Kennt ihr alle die Regeln? (y/n)");

        while (true)
        {
            string input1 = Console.ReadLine();
            if (input1 == "y")
            {
                Console.WriteLine("Gut, dann können wir ja direkt beginnen!");
                Thread.Sleep(1000);
                Console.Clear();
                Start();
            }
            else if (input1 == "n")
            {
                view.Rules();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Gebe 'y' oder 'n' ein.");
                Console.ResetColor();
            }
        }
    }

    static void Start()
    {
        player.Reset = 0;
        int startingPlayer = ChooseStartingPlayer();
        Console.WriteLine(players[startingPlayer].Name + " beginnt!");
        Thread.Sleep(2000);
        Console.Clear();

        List<string> colors = new List<string> { "Red", "Green", "Blue", "Yellow" };
        List<string> values = Enumerable.Range(0, 10).Select(i => i.ToString()).Concat(new string[] { "Skip", "+2", "Reverse" }).ToList();
        List<string> specialCards = new List<string> { "Wild ", "Draw 4" };

        List<string> deck = GenerateDeck(colors, values, specialCards);
        ShuffleDeck(deck);

        foreach (Players player in players)
        {
            player.Hand = DealCards(deck, 7);
        }

        string firstCard = PlaceFirstCardInCenter(deck, center);

        while (true)
        {
            if (player.Reset == 0)
            {
                for (player.I = startingPlayer; player.I <= 3; player.I++)
                {
                    Game();
                }
                startingPlayer = 0;
            }
            else if (player.Reset == 1)
            {
                for (player.I = startingPlayer; player.I >= 0; player.I--)
                {
                    Game();
                }
                startingPlayer = 3;
            }
        }

        void Game()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(players[player.I].Name);
            
            Console.ResetColor();
            Console.WriteLine(", du bist dran.");
            Console.ResetColor();

            if (center.Count > 0)
            {
                ConsoleColor consoleColor;
                string[] centerCardColor = center.Last().Split(' ');

                if (Enum.TryParse(centerCardColor[0], true, out consoleColor))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("Karte auf dem Ablagestapel: ");
                    Console.ForegroundColor = consoleColor;
                    Console.WriteLine(center.Last());
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("Ungültige Farbe. Verwende die Standardfarbe.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Der Ablagestapel ist noch leer.");
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Deine Karten:");
            Console.ResetColor();
            PrintHand(players[player.I].Hand);

            bool validCardPlayed = false;

            while (!validCardPlayed)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Wähle eine Karte zum Spielen oder drücke Enter um eine Karte zu ziehen:");
                Console.ResetColor();

                string input = Console.ReadLine();

                
                if (string.IsNullOrWhiteSpace(input))
                {
                    string drawnCard = DrawCard(deck);
                    players[player.I].Hand.Add(drawnCard);
                    Console.WriteLine(players[player.I].Name + " hat eine Karte gezogen: " + drawnCard);
                    validCardPlayed = true;
                    Console.WriteLine("Drücke Enter zum fortsetzen: ");
                    Console.ReadLine();
                }
                else if (int.TryParse(input, out int cardIndex) && cardIndex >= 0 && cardIndex < players[player.I].Hand.Count)
                {
                    string selectedCard = players[player.I].Hand[cardIndex];

                    if (IsCardPlayable(selectedCard, center))
                    {
                        if (IsTwoPlus(selectedCard))
                        {
                            int number = 0;
                            for (number = 0; number < 2; number++)
                            {
                                int nextPlayerIndex = (player.I + 1) % players.Count;
                                if (nextPlayerIndex < players.Count)
                                {
                                    string drawnCard = DrawCard(deck);
                                    players[nextPlayerIndex].Hand.Add(drawnCard);
                                    Console.WriteLine(players[nextPlayerIndex].Name + " hat eine Karte gezogen: " + drawnCard);
                                    validCardPlayed = true;
                                }
                            }
                            Console.WriteLine("Drücke Enter zum fortsetzen: ");
                            Console.ReadLine();
                        }
                        if (IsSkip(selectedCard))
                        {
                            int nextPlayerIndex = (player.I + 1) % players.Count;
                            Console.WriteLine(players[nextPlayerIndex].Name + " wird übersprungen!");
                            validCardPlayed = false;
                            player.I = nextPlayerIndex;
                            Console.WriteLine("Drücke Enter zum fortsetzen: ");
                            Console.ReadLine();
                        }


                        if (IsReverse(selectedCard))
                        {
                            if (player.Reset == 0)
                            {
                                player.Reset += 1;
                                player.I = 3;
                            }
                            else if (player.Reset == 1)
                            {
                                player.Reset = 0;
                                player.I = 0;
                            }
                            else
                            {
                                Console.WriteLine("FEHLER");
                            }
                            Console.WriteLine("Richtung wurde geändert!");
                            validCardPlayed = false;
                            Console.WriteLine("Drücke Enter zum fortsetzen: ");
                            Console.ReadLine();
                        
                        }
                        if (IsWildcard(selectedCard))
                        {
                            string cards = selectedCard;
                            Console.WriteLine("Welche Farbe möchtest du wählen (Red, Green, Blue, Yellow)?");
                            string chosenColor = Console.ReadLine();
                            if (IsValidColor(chosenColor) && cards.Contains("Draw 4"))
                            {
                                selectedCard = chosenColor + " " + "+4";
                            }
                            else if (IsValidColor(chosenColor))
                            {
                                selectedCard = chosenColor + " " + "Wild";
                            }
                            else
                            {
                                Console.WriteLine("Ungültige Farbwahl. Die Karte wurde nicht gespielt.");
                                continue;
                            }
                            if (cards.Contains("Draw 4"))
                            {
                                int number = 0;
                                for (number = 0; number < 4; number++)
                                {
                                    int nextPlayerIndex = (player.I + 1) % players.Count;
                                    if (nextPlayerIndex < players.Count)
                                    {
                                        string drawnCard = DrawCard(deck);
                                        players[nextPlayerIndex].Hand.Add(drawnCard);
                                        Console.WriteLine(players[nextPlayerIndex].Name + " hat eine Karte gezogen: " + drawnCard);
                                        validCardPlayed = false;
                                    }
                                }
                                Console.WriteLine("Drücke Enter zum fortsetzen: ");
                                Console.ReadLine();
                            }

                        }

                        center.Add(selectedCard);
                        players[player.I].Hand.RemoveAt(cardIndex);
                        Console.WriteLine(players[player.I].Name + " hat " + selectedCard + " gespielt.");
                        validCardPlayed = true;
                    }
                    else
                    {
                        Console.WriteLine("Du kannst die ausgewählte Karte nicht spielen. Wähle eine andere Karte.");
                    }
                }
                else
                {
                    Console.WriteLine("Ungültige Eingabe. Du musst einen gültigen Index eingeben oder Enter drücken.");
                }
            }

            if (players[player.I].Hand.Count == 1)
            {
                Console.WriteLine(players[player.I].Name + " ruft UNO!");
            }

            if (players[player.I].Hand.Count == 0)
            {
                Console.WriteLine(players[player.I].Name + " hat gewonnen!");
                return;
            }

            Thread.Sleep(100);
            Console.Clear();
        }
    }
        static int ChooseStartingPlayer()
        {
            return random.Next(0,4);
        }

        static List<string> GenerateDeck(List<string> colors, List<string> values, List<string> specialCards)
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
                for (int i = 0; player.I < 4; player.I++)
                {
                    deck.Add(specialCard);
                }
            }

            return deck;
        }

        static void ShuffleDeck(List<string> deck)
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

        static List<string> DealCards(List<string> deck, int handSize)
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

        static void PrintHand(List<string> hand)
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

        static string PlaceFirstCardInCenter(List<string> deck, List<string> center)
        {
            int randomCard = random.Next(deck.Count);
            string selectedCard = deck[randomCard];
            deck.RemoveAt(randomCard);
            center.Add(selectedCard);

            return selectedCard;
        }

        static string DrawCard(List<string> deck)
        {
            string card = deck.First();
            deck.RemoveAt(0);
            return card;
        }

        static bool IsCardPlayable(string selectedCard, List<string> center)
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
        static bool IsTwoPlus(string card)
        {
            return card.Contains("+2");
        }
        static bool IsSkip(string card)
        {
            return card.Contains("Skip");
        }
        static bool IsReverse(string card)
        {
            return card.Contains("Reverse");
        }
        static bool IsWildcard(string card)
        {
            return card.Contains("Wild") || card.Contains("Draw 4");
        }
        static bool IsValidColor(string color)
        {
            List<string> validColors = new List<string> { "Red", "Green", "Blue", "Yellow" };
            return validColors.Contains(color);
        }
    }