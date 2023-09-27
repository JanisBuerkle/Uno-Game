public class View
{
    public void Rules()
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