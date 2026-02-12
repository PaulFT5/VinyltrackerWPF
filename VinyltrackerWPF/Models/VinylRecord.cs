using System.Collections.Generic;
namespace VinylTrackerWPF.Models;

public class VinylRecord
{
    public string? Id { get; set; }
    public string Artist { get; set; } = string.Empty;
    public string Album { get; set; } = string.Empty;
    public string Year { get; set; } = string.Empty;
    public List<Track> Tracks { get; set; } = new List<Track>();
}