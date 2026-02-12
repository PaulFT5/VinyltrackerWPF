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
}