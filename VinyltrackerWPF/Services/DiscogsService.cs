using RestSharp;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylTrackerWPF.Models;

namespace VinylTrackerWPF.Services;

public class DiscogsService
{
    private readonly RestClient _client;
    private const string Token = "ZdGfhmsNqHPHyDPXyBDknPzHbqCLxLdXHspXrDBd";

    public DiscogsService()
    {
        _client = new RestClient("https://api.discogs.com/");
    }

    public async Task<List<DiscogsSearchResult>> SearchAlbumsAsync(string query)
    {
        var request = new RestRequest("database/search");
        request.AddParameter("q", query);
        request.AddParameter("type", "release");

        request.AddHeader("Authorization", $"Discogs token={Token}");
        request.AddHeader("User-Agent", "VinylTrackerWPF/1.0");

        var response = await _client.GetAsync(request);

        if (!response.IsSuccessful || string.IsNullOrEmpty(response.Content))
            return new List<DiscogsSearchResult>();

        var data = JObject.Parse(response.Content);

        return data["results"]?.Select(r => new DiscogsSearchResult
        {
            Id = (int)r["id"],
            Title = r["title"]?.ToString() ?? "Unknown",
            Year = r["year"]?.ToString() ?? "N/A",
            Thumb = r["thumb"]?.ToString() ?? ""
        }).ToList() ?? new List<DiscogsSearchResult>();
    }

    public async Task<VinylRecord?> GetFullReleaseDetailsAsync(int discogsId)
    {
        var request = new RestRequest($"releases/{discogsId}");
        request.AddHeader("Authorization", $"Discogs token={Token}");
        request.AddHeader("User-Agent", "VinylTrackerWPF/1.0");

        var response = await _client.GetAsync(request);
        if (!response.IsSuccessful || string.IsNullOrEmpty(response.Content)) return null;

        var data = JObject.Parse(response.Content);

        var masterID = data["master_id"]?.ToString(); //Use master id to find an album first release date, number returned as string
        return new VinylRecord
        {
            DiscogsId = discogsId,
            Artist = data["artists"]?[0]?["name"]?.ToString() ?? "Unknown",
            Album = data["title"]?.ToString() ?? "Unknown",

            //Year = data["year"]?.ToString() ?? "N/A",
            Year = data["year"]?.ToString() ?? 
                    data["released"]?.ToString() ?? "N/A",

            Genre = data["genres"]?[0]?.ToString() ?? "Unknown",

            ImageUrl = data["images"]?[0]?["resource_url"]?.ToString(), 

            RecomandedPrice = data["lowest_price"]?.ToString() ?? "N/A",

            Tracks = data["tracklist"]?.Select(t => new Track //make it take only vinyls
            {
                Title = t["title"]?.ToString() ?? "Unknown Track",
                Position = t["position"]?.ToString() ?? "",
                Duration = t["duration"]?.ToString() ?? ""
            }).ToList() ?? new List<Track>()
        };
    }
}