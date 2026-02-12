namespace VinylTrackerWPF.Models;

public class Track
{
    public string Title { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public string Side { get; set; } = string.Empty;
    public int TrackNumber { get; set; }
}