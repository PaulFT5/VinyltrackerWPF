namespace VinylTrackerWPF.Models;

public class VinylRecord
{
    public string? Id { get; set; } // Firebase internal ID
    public int DiscogsId { get; set; } // Unique ID from the Discogs Database
    public string Artist { get; set; } = string.Empty; //
    public string Album { get; set; } = string.Empty; //
    public string Year { get; set; } = string.Empty; //
    public string? ImageUrl { get; set; } // URL for the album cover
    public List<Track> Tracks { get; set; } = new List<Track>(); //
    public string MinPrice { get; set; } = "N/A";
    public string MaxPrice { get; set; } = "N/A";
    public string MedianPrice { get; set; } = "N/A";
}