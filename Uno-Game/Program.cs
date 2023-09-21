

class Program
{
    static Random random = new Random();
    static List<string> center = new List<string>();


    static void Main(string[] args)
    {

        // Create Lists
        List<string> colors = new List<string> { "Red", "Green", "Blue", "Yellow" };
        List<string> values = new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "Skip", "Draw 2", "Reverse" };
        List<string> specialCards = new List<string> { "Wild", "Draw 4" };

        // Generate together colors + " " + values = colors values // example: Red + " " + 0 = Red 0
        List<string> deck = GenerateDeck(colors, values, specialCards);

        // Schuffle Method
        ShuffleDeck(deck);

        // Player`s get cards
        List<string> player1Hand = DealCards(deck, 7);
        List<string> player2Hand = DealCards(deck, 7);
        List<string> player3Hand = DealCards(deck, 7);
        List<string> player4Hand = DealCards(deck, 7);

        // Player One Print Cards
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Spieler 1: ");
        Console.ResetColor();
        PrintHand(player1Hand);

        // Player Two Print Cards
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Spieler 2: ");
        Console.ResetColor();
        PrintHand(player2Hand);

        // Player Two Print Cards
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Spieler 3: ");
        Console.ResetColor();
        PrintHand(player3Hand);

        // Player Two Print Cards
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Spieler 4: ");
        Console.ResetColor();
        PrintHand(player4Hand);

        string firstCard = PlaceFirstCardInCenter(deck, center);

        // if firstcard is not Empty then Print the fist Card
        if (firstCard != null)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Die erste Karte in der Mitte ist: " + firstCard);
            Console.ResetColor();
        }

        // choose starting player
        int startingPlayer = ChooseStartingPlayer();
        // print starting player
        Console.WriteLine("Spieler " + startingPlayer + " beginnt!");
    }

    // Generate go trought every Card and create all Cards
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

        // Create Special cards (Wild, Draw 4)
        foreach (string specialCard in specialCards)
        {
            for (int i = 0; i < 4; i++)
            {
                deck.Add(specialCard);
            }
        }

        return deck;
    }

    // Shuffle the Cards
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

    // Deal 7 cards to players 
    static List<string>DealCards(List<string> deck, int handSize)
    {
        List<string> hand = new List<string>();
        for (int i = 0; i < handSize; i++)
        {
            string card = deck.First();
            deck.RemoveAt(0);
            hand.Add(card);
        }
        return hand;
    }

    // Print every card from the player in the Console
    static void PrintHand(List<string> hand)
    {
        foreach (string card in hand)
        {
            Console.WriteLine(card);
        }
    }

    // random Card from deck
    static string PlaceFirstCardInCenter(List<string> deck, List<string> center)
    {

        int randomCard = random.Next(deck.Count);
        string selectedCard = deck[randomCard];
        deck.RemoveAt(randomCard);
        center.Add(selectedCard);

        return selectedCard;
    }

    // random number between 1-4
    static int ChooseStartingPlayer()
    {
        return random.Next(1, 5); // Generiert eine zufällige Zahl zwischen 1 und 4
    }

}
