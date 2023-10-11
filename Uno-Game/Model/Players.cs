using System.Runtime;

public class Players
{
    public string Name { get; set; }
    public int Reset { get; set; }
    public int Player { get; set; }
    public int PlayerSave { get; set; }
    public int ReverseCardPlayed { get; set; }
    public int NextPlayer { get; set; }
    public int CountOfPlayers { get; set; }
    public List<string> Hand { get; set; }

    public Players()
    {
        Hand = new List<string>();
    }
}