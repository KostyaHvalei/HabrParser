namespace HabrParser.Models;

public class LoadInfo
{
    public int Id { get; set; }
    public int CountLoaded { get; set; }
    public DateTime LoadedAt { get; set; }
    public bool LoadedAutomatically { get; set; }
}