﻿using System.Xml;

namespace Uno_Game
{
    public class UnoView
    {
        //static UnoModel model = new UnoModel();
        private readonly UnoModel model;

        public UnoView(UnoModel model)
        {
            this.model = model;
        }

        public void Start()
        {
            model.player.Richtung = 0;
            Console.WriteLine("Willkommen bei UNO!");
        }

        public int ChoosePlayerCount()
        {
            int i = 0;
            while (i == 0)
            {
                Console.WriteLine("Mit wie vielen Spielern wollt ihr spielen? (2-5)");
                string CountOfPlayersEingabe = Console.ReadLine();
                int CountOfPlayersConvert;
                if (int.TryParse(CountOfPlayersEingabe, out CountOfPlayersConvert) && CountOfPlayersConvert >= 2 && CountOfPlayersConvert <= 5)
                {
                    model.player.CountOfPlayers = CountOfPlayersConvert;
                    Console.WriteLine("Anzahl der Spieler: " + model.player.CountOfPlayers);
                    i = 1;
                }
                else
                {
                    Fehler();
                }
            }

            return model.player.CountOfPlayers;
        }

        public int ChoosePlayerNames()
        {
            int zahl = 0;
            for (int playerNumb = 0; playerNumb < model.player.CountOfPlayers; playerNumb++)
            {
                zahl++;
                bool success = false;
                while (!success)
                {
                    Console.WriteLine("Gib den {0}. Namen ein:", zahl);
                    string player = Console.ReadLine();
                    int playeranzahl = model.players.Count;
                    bool addName = true;
                    bool noError = false;
                    if (playeranzahl == 0)
                    {
                        if (player.Contains(" ") || player == "")
                        {
                            success = false;
                            addName = false;
                        }
                        else
                        {
                            model.players.Add(new Players() { Name = player });
                            success = true;
                            addName = false;
                            noError = true;
                        }
                    }
                    else
                    {
                        foreach (Players playerInList in model.players)
                        {
                            if (player.Contains(" ") || player == "")
                            {
                                addName = false;
                            }
                            else if (player == playerInList.Name)
                            {
                                success = false;
                                addName = false;
                            }
                        }
                    }

                    if (addName)
                    {
                        model.players.Add(new Players() { Name = player });
                        success = true;
                    }
                    else
                    {
                        if (noError)
                        {

                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Ungültige Eingabe. Bitte überprüfe deine Eingabe!");
                            Console.ResetColor();
                        }
                    }
                }
            }

            return model.player.CountOfPlayers;
        }

        public void RuleQuery()
        {
            Console.WriteLine("Da wir dies nun haben fangen wir doch direkt an!");
            Console.WriteLine("Kennt ihr alle die Regeln? (y/n)");

            while (true)
            {
                string RuleQueryInput = Console.ReadLine();
                switch (RuleQueryInput)
                {
                    case "y":
                        Console.WriteLine("Gut, dann können wir ja direkt beginnen!");
                        Clear();
                        return;
                        break;
                    case "n":
                        Rules();
                        break;
                    default:
                        Fehler();
                        break;

                }
            }
        }

        public void Fehler()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ungültige Eingabe. Bitte überprüfe deine Eingabe!");
            Console.ResetColor();
        }

        public void Clear()
        {
            Thread.Sleep(1000);
            Console.Clear();
        }

        public void PlayerDisplay()
        {

            
            int FirstCardShow = model.player.Center.Count;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Der letzte Spieler hat ");
            if (model.player.Draw)
            {
                Console.WriteLine("eine Karte gezogen.");
                model.player.Draw = false;
            }
            else
            {
                ConsoleColor consoleColor;
                string played = model.player.Center[FirstCardShow - 1];
                string[] cardColor = played.Split(' ');

                if (Enum.TryParse(cardColor[0], true, out consoleColor))
                {

                    Console.Write("eine Karte gespielt: ");
                    Console.ForegroundColor = consoleColor;
                    Console.WriteLine(played);
                    Console.ResetColor();
                }
                else if (played.Contains("Wild") || played.Contains("Draw 4"))
                {
                    Console.Write("eine Karte gespielt: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(played);
                    Console.ResetColor();
                }
            }
            Console.ResetColor();
            
            Console.Write("Nächster Spieler: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            if (model.player.Richtung == 0)
            {
                int next = (model.player.Player + 1) % model.players.Count;
                Console.WriteLine(model.players[next].Name);
            }
            else if (model.player.Richtung == 1)
            {
                int next = (model.player.Player == 0)
                    ? (model.players.Count - 1)
                    : (model.player.Player - 1) % model.players.Count;
                Console.WriteLine(model.players[next].Name);
            }
            Console.Write(model.players[model.player.Player].Name);
            model.player.PlayerSave = model.player.Player;
            Console.ResetColor();
            Console.WriteLine(", du bist dran.");
            Console.ResetColor();
        }
        public void Center()
        {
            if (model.player.Center.Count > 0)
            {
                ConsoleColor consoleColor;
                string[] centerCardColor = model.player.Center.Last().Split(' ');

                if (Enum.TryParse(centerCardColor[0], true, out consoleColor))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("Karte auf dem Ablagestapel: ");
                    Console.ForegroundColor = consoleColor;
                    Console.WriteLine(model.player.Center.Last());
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
        }
        public void CardDisplay()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Deine Karten:");
            Console.ResetColor();
            model.PrintHand(model.players[model.player.Player].Hand);
        }


        public void GameTurn()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Wähle eine Karte zum Spielen oder drücke Enter um eine Karte zu ziehen:");
            Console.ResetColor();
        }


        public void Play()
        {
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                model.player.Draw = true;
                string drawnCard = model.DrawCard();
                model.players[model.player.Player].Hand.Add(drawnCard);

                ConsoleColor consoleColor;
                string[] cardColor = drawnCard.Split(' ');

                if (Enum.TryParse(cardColor[0], true, out consoleColor))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(model.players[model.player.Player].Name);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(" hat eine Karte gezogen: ");
                    Console.ForegroundColor = consoleColor;
                    Console.WriteLine(drawnCard);
                    Console.ResetColor();
                }
                else if (drawnCard.Contains("Wild") || drawnCard.Contains("Draw 4"))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(model.players[model.player.Player].Name);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(" hat eine Karte gezogen: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(drawnCard);
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ALARM!!! ALARM!!");
                }

                model.player.validCardPlayed = true;
                Enter();
            }
            else if (int.TryParse(input, out int cardIndex) && cardIndex >= 0 &&
                         cardIndex < model.players[model.player.Player].Hand.Count)
                {
                    string selectedCard = model.players[model.player.Player].Hand[cardIndex];

                    if (model.IsCardPlayable(selectedCard))
                    {
                        if (model.IsTwoPlus(selectedCard))
                        {
                            int number = 0;
                            int nextPlayerIndex = -1;

                            for (number = 0; number < 2; number++)
                            {
                                if (model.player.Richtung == 0)
                                {
                                    nextPlayerIndex = (model.player.Player + 1) % model.players.Count;
                                }
                                else if (model.player.Richtung == 1)
                                {
                                    if (model.player.Player == 0)
                                    {
                                        nextPlayerIndex = model.players.Count - 1;
                                    }
                                    else
                                    {
                                        nextPlayerIndex = (model.player.Player - 1) % model.players.Count;
                                    }
                                }

                                if (nextPlayerIndex >= 0 && nextPlayerIndex < model.players.Count)
                                {
                                    string drawnCard = model.DrawCard();
                                    model.players[nextPlayerIndex].Hand.Add(drawnCard);
                                    // Console.WriteLine(model.players[nextPlayerIndex].Name + " hat eine Karte gezogen: " + drawnCard);
                                    model.player.validCardPlayed = true;
                                }
                            }
                        }

                        if (model.IsSkip(selectedCard))
                        {
                            int nextPlayerIndex = 0;
                            if (model.player.Richtung == 0)
                            {
                                nextPlayerIndex = (model.player.Player + 1) % model.players.Count;
                            }
                            else if (model.player.Richtung == 1)
                            {
                                if (model.player.Player == 0)
                                {
                                    nextPlayerIndex = model.players.Count - 1;
                                }
                                else
                                {
                                    nextPlayerIndex = (model.player.Player - 1) % model.players.Count;
                                }

                            }

                            Console.WriteLine(model.players[nextPlayerIndex].Name + " wird übersprungen!");
                            model.player.validCardPlayed = true;
                            model.player.Player = nextPlayerIndex;
                        }

                        if (model.IsReverse(selectedCard))
                        {
                            model.player.ReverseCardPlayed = 1;
                            if (model.player.Richtung == 0)
                            {
                                model.player.Richtung = 1;
                                model.player.NextPlayer = (model.player.Player - 1) % model.players.Count;
                                model.player.Player = model.player.CountOfPlayers - 1;
                            }
                            else if (model.player.Richtung == 1)
                            {
                                model.player.Richtung = 0;
                                model.player.NextPlayer = (model.player.Player + 1) % model.players.Count;
                                model.player.Player = 0;
                            }
                            else
                            {
                                Console.WriteLine("FEHLER");
                            }

                            Console.WriteLine("Richtung wurde geändert!");
                            model.player.validCardPlayed = true;
                        }

                        if (model.IsWildcard(selectedCard))
                        {
                            string cards = selectedCard;
                            while (!model.player.validCardPlayed)
                            {
                            Console.WriteLine("Welche Farbe möchtest du wählen (Red, Green, Blue, Yellow)?");
                            string colorInput = Console.ReadLine();
                            string chosenColor = "";
                            switch (colorInput.ToLower())
                            {
                                case "red":
                                    chosenColor = "Red";
                                    break;
                                case "green":
                                    chosenColor = "Green";
                                    break;
                                case "blue":
                                    chosenColor = "Blue";
                                    break;
                                case "yellow":
                                    chosenColor = "Yellow";
                                    break;
                            }


                                if (model.IsValidColor(chosenColor) && cards.Contains("Draw 4"))
                                {
                                    selectedCard = chosenColor + " " + "+4";
                                    model.player.validCardPlayed = true;
                                }
                                else if (model.IsValidColor(chosenColor))
                                {
                                    selectedCard = chosenColor + " " + "Wild";
                                    model.player.validCardPlayed = true;
                                }
                                else
                                {
                                    Console.WriteLine("Ungültige Farbwahl. Die Karte wurde nicht gespielt.");
                                    model.player.validCardPlayed = false;
                                }
                            }
                            

                            if (cards.Contains("Draw 4"))
                            {
                                int number = 0;
                                int nextPlayerIndex = -1;
                                for (number = 0; number < 4; number++)
                                {
                                    if (model.player.Richtung == 0)
                                    {
                                        nextPlayerIndex = (model.player.Player + 1) % model.players.Count;
                                    }
                                    else if (model.player.Richtung == 1)
                                    {
                                        if (model.player.Player == 0)
                                        {
                                            nextPlayerIndex = model.players.Count - 1;
                                        }
                                        else
                                        {
                                            nextPlayerIndex = (model.player.Player - 1) % model.players.Count;
                                        }
                                    }

                                    if (nextPlayerIndex >= 0 && nextPlayerIndex < model.players.Count)
                                    {
                                        string drawnCard = model.DrawCard();
                                        model.players[nextPlayerIndex].Hand.Add(drawnCard);
                                        // Console.WriteLine(model.players[nextPlayerIndex].Name + " hat eine Karte gezogen: " + drawnCard);
                                        model.player.validCardPlayed = true;
                                    }
                                }
                            }
                        }

                        model.player.Center.Add(selectedCard);
                        
                        model.players[model.player.PlayerSave].Hand.RemoveAt(cardIndex);
                        Console.WriteLine(model.players[model.player.PlayerSave].Name + " hat " + selectedCard + " gespielt.");
                        model.player.validCardPlayed = true;
                        Enter();
                    }
                    else
                    {
                        Console.WriteLine(
                            "Du kannst die ausgewählte Karte nicht spielen. Wähle eine andere Karte.");
                    }
                }
                else
                {
                    Console.WriteLine(
                        "Ungültige Eingabe. Du musst einen gültigen Index eingeben oder Enter drücken.");
                }
        }
        public void Game()
        {

            
            if (model.players[model.player.Player].Hand.Count == 1)
            {
                Console.WriteLine(model.players[model.player.Player].Name + " ruft UNO!");
            }

            if (model.players[model.player.Player].Hand.Count == 0)
            {
                Console.WriteLine(model.players[model.player.Player].Name + " hat gewonnen!");
                return;
            }

            Thread.Sleep(100);
            Console.Clear();
        }
        
        
        
        private void Enter()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Drücke ");
            Console.ResetColor();
            Console.Write("Enter");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(" zum fortsetzen: ");
            Console.ResetColor();
            Console.ReadLine();
            Console.Clear();
        }
        void Rules()
        {
            Console.Clear();
            Console.WriteLine("Die Originalen UNO-Spielregeln: \n" +
                              "Karten: \n" +
                              "108 Karten \n" +
                              "19 blaue Karten \n" +
                              "19 grüne Karten \n" +
                              "19 rote Karten" +
                              "19 gelbe Karten\n" +
                              "(2 x 1 - 9 , 1 x 0)\n" +
                              "8 ziehe zwei karten(2 für jede Farbe)\n" +
                              "8 Retour karten(2 für jede Farbe)\n" +
                              "8 Aussetzen karten(2 für jede Farbe)\n" +
                              "4 Farbenwahlkarten\n" +
                              "4 ziehe 4 Farbenwahlkarten\n" +
                              "Start des Spiels:\n" +
                              "Jeder Spieler erhält 7 Karten und eine Karte wird Zufällig auf den Tisch gelegt.\n" +
                              "Im ersten Spiel wird ein Spieler Zufällig auserwählt und dann im Uhrzeiger Richtung weiter.\n" +
                              "Hält ein Spieler keine Karte in der Hand, die er auf den Stapel legen könnte, muss er vom Kartenstock eine Karte ziehen.\n" +
                              "Er darf dir gezogene Karte dann sofort legen, wenn er kann.\n" +
                              "Ansonsten setzt der nächste Spieler das Spiel fort.\n" +
                              "Die Spieler haben die Möglichkeit, eine ablegbare Karte nicht abzulegen.\n" +
                              "Wenn dies der Fall ist, muss der Spieler vom Kartenstock eine Karte ziehen.\n" +
                              "Er darf die gezogene Karte sofort legen, wenn er kann.\n" +
                              "Der Spieler darf jedoch nach dem Ziehen Keine Karte, die er bereits in der Hand hält, legen.\\n\r\n" +
                              "Ausspielen:\n" +
                              "Hat ein Spieler nur noch eine Karte auf seiner Hand übrig muss er “UNO“ Rufen (Klicken), vergisst er dieses, muss er 2 Strafkarten vom Kartenstock ziehen.\n" +
                              "Er muss diese Strafkarten jedoch nur ziehen, wenn er von den anderen Spielern erwischt wird.\n" +
                              "Ziel des Spiels:\n" +
                              "Ziel ist es als erster Spieler alle Karten auf den Stapel legen.\n" +
                              "Funktion der Aktionskarten:\n" +
                              "Zieh Zwei Karte:\n" +
                              "Wenn diese Karte gelegt wird, muss der nächste Spieler 2 Karten ziehen und darf in der Runde keine Karte ablegen.\n" +
                              "Auf diese Karte kann nur noch eine gleich farbige karte oder eine andere zieh zwei Karte.\n" +
                              "Retour Karte:\n" +
                              "Bei dieser Karte ändert sich die Spielrichtung.\n" +
                              "Wenn bisher nach links gespielt wird nun nach rechts gespielt und umgekehrt.\n" +
                              "Die Karte kann nur auf eine entsprechende Farbe oder eine andere Retour Karte gelegt werden.\n" +
                              "Wenn diese zu Beginn des Spiels gezogen wird, fängt der Geber an und dann setzt der Spieler zu seiner Rechten anstatt zu seiner linken das Spiel fort.\n" +
                              "Aussetzen Karte:\n" +
                              "Nachdem diese Karte gelegt wurde, wird der nächste Spieler “übersprungen“.\n" +
                              "Die Karte kann nur auf eine Karte mit entsprechender Farbe oder eine andere Aussetzen Karte gelegt werden.\n" +
                              "Wenn diese Karte zu Beginn des Spiels gezogen wird, wird der Start Spieler “übersprungen“ und der nächste Spieler setzt das Spiel fort.\n" +
                              "Farbenwahl Karte:\n" +
                              "Der Spieler, der diese Karte legt, entscheidet, welche Farbe als nächstes gelegt werden soll.\n" +
                              "Auch die schon liegende Farbe darf gewählt werden, wenn der Spieler dieses so will.\n" +
                              "Wenn diese Karte zu beginn kommen sollte, darf der Spieler entscheiden, welche Farbe als nächstes gelegt werden soll.\n" +
                              "Plus 4 Karte:\n" +
                              "Der Spieler der sie Legt entscheidet welche Farbe als nächste gelegt werden soll.\n" +
                              "Zudem muss der nächste Spieler 4 Karten ziehen und darf nicht ablegen.\n" +
                              "(Leider darf die Karte nur dann gelegt werden wenn der Spieler der sie legt keine andere Karte auf den Stapel legen könnte)\n" +
                              "UNO mit zwei Spielern:\n" +
                              "1. Wird eine Retour Karte abgelegt hat das dieselben Folgen wie eine Aussetzen Karte.\n" +
                              "2. Der Spieler der eine Aussetzen Karte ablegt kann sofort noch eine Karte ablegen.\n" +
                              "3. Legt ein Spieler eine Zieh zwei Karte ab und hat der andere Spieler die 2 Karten gezogen, ist der erste Spieler wieder an der Reihe.\n" +
                              "Dasselbe gilt für die Zieh Vier Farbenwahlkarte.\n" +
                              "In allen anderen Fällen gelten die Normalen Uno-Regeln.\n\n" +
                              "Wenn du alles verstanden hast, gib 'y' ein.\n");
        }
    }
}