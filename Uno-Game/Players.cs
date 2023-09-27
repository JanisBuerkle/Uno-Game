public class Players
{
    public string Name { get; set; }
    //public string PlayerAnzahl { get; set; }
    public List<string> Hand { get; set; }

    public Players()
    {
        Hand = new List<string>();
    }
}
