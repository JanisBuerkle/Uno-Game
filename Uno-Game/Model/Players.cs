using System.Runtime;

public class Players
{
    public string Name { get; set; }
    public int Rechnung { get; set; }
    public int Anzahl { get; set; }
    public int Reset { get; set; }
    public int I { get; set; }
    public int P { get; set; }
    public int Test { get; set; }
    public int NextPlayer { get; set; }
    public List<string> Hand { get; set; }

    public Players()
    {
        Hand = new List<string>();
    }
}