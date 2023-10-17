public class Players
{
    public string Name { get; set; }
    public int Richtung { get; set; }
    public int Player { get; set; }
    public int PlayerSave { get; set; }
    public int ReverseCardPlayed { get; set; }
    public int NextPlayer { get; set; }
    public int CountOfPlayers { get; set; }
    public List<string> Hand { get; set; }
    public List<string> Deck { get; set; }
    public List<string> Center { get; set; }
    public int StartingPlayer { get; set; }
    public bool validCardPlayed { get; set; }
    public bool Draw { get; set; }
    public Players()
    {
        Hand = new List<string>();
        Center = new List<string>();
    }
}