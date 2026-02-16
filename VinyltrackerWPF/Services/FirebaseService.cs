using Firebase.Database;
using Firebase.Database.Query;
using System.Threading.Tasks;
using VinylTrackerWPF.Models;

namespace VinylTrackerWPF.Services;


    public class FirebaseService
{
    private readonly FirebaseClient _client;

    public FirebaseService()
    {
        _client = new FirebaseClient("https://vinyltracker-9f84c-default-rtdb.europe-west1.firebasedatabase.app/");
    }

    public async Task AddVinylAsync(VinylRecord vinyl, string userId)
    {
        await _client
            .Child("Users")
            .Child(userId) // Use the dynamic ID from AuthService
            .Child("Vinyls")
            .PostAsync(vinyl);
    }


    //get vinyl for user
    public async Task<List<VinylRecord>> GetUserVinylsAsync(string userId)
    {
        var vinyls = await _client
            .Child("Users")
            .Child(userId)
            .Child("Vinyls")
            .OnceAsync<VinylRecord>();

        return vinyls.Select(item => new VinylRecord
        {
            // If your Model has an 'Id' property, map the Firebase Key to it
            Id = item.Key,
            Artist = item.Object.Artist,
            Album = item.Object.Album,
            Year = item.Object.Year,
            RecomandedPrice = item.Object.RecomandedPrice,
            Genre = item.Object.Genre,
            // ... map other properties as needed
        }).ToList();
    }
}