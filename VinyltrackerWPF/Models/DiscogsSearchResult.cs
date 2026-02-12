namespace VinylTrackerWPF.Models;

public class DiscogsSearchResult
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty; // Usually "Artist - Album"
    public string Year { get; set; } = string.Empty;
    public string Thumb { get; set; } = string.Empty; // Small 150x150 image URL
}