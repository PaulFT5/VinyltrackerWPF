namespace VinylTrackerWPF.Models;

public class Track
{
    public string Title { get; set; } = string.Empty; //
    public string Duration { get; set; } = string.Empty; //
    public string Position { get; set; } = string.Empty; // Maps to Discogs 'A1', 'B1', etc.
}