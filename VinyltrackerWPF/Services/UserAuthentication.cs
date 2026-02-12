using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using System.Threading.Tasks;

namespace VinylTrackerWPF.Services;

public class AuthService
{
    private readonly FirebaseAuthClient _authClient;

    public AuthService()
    {
        var config = new FirebaseAuthConfig
        {
            ApiKey = "AIzaSyChL_ic12APQ4SUl-7W-vuE29UVVljWIQ8",
            AuthDomain = "vinyltracker-9f84c.firebaseapp.com",

            Providers = new FirebaseAuthProvider[]
            {
                new GoogleProvider().AddScopes("email"),
                new EmailProvider()
            },

            UserRepository = new FileUserRepository("VinylTrackerApp")
        };

        _authClient = new FirebaseAuthClient(config);
    }

    public bool IsAuthenticated => _authClient.User != null;

    public FirebaseAuthClient GetClient() => _authClient;

    public async Task<string> LoginAsync(string email, string password)
    {
        var userCredential = await _authClient.SignInWithEmailAndPasswordAsync(email, password);
        return await userCredential.User.GetIdTokenAsync();
    }

    public async Task<string> RegisterAsync(string email, string password, string displayName)
    {
        var userCredential = await _authClient.CreateUserWithEmailAndPasswordAsync(email, password, displayName);
        return await userCredential.User.GetIdTokenAsync();
    }

    public void LogOut()
    {
        _authClient.SignOut();
    }
}