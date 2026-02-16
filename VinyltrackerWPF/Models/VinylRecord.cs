namespace VinylTrackerWPF.Models;

public class VinylRecord
{
    public string? Id { get; set; } // Firebase internal ID
    public int DiscogsId { get; set; } // Unique ID from the Discogs Database
    public string Artist { get; set; } = string.Empty; //
    public string Album { get; set; } = string.Empty; //
    public string Year { get; set; } = string.Empty; //
    public string? ImageUrl { get; set; } // URL for the album cover
    public List<Track> Tracks { get; set; } = new List<Track>(); // take only vinyls
    public string RecomandedPrice { get; set; } = "N/A";

    public string Genre { get; set; } = string.Empty;
}