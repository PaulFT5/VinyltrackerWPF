using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VinylTrackerWPF.Services;
using VinyltrackerWPF.Page;

namespace VinyltrackerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AuthService _auth = new AuthService();
        FirebaseService _firebaseService = new FirebaseService();
        DiscogsService _discogsService = new DiscogsService();
        public MainWindow()
        {
            InitializeComponent();
            CheckAuth();
        }

        private void CheckAuth() { 
                if (!_auth.IsAuthenticated)
                {
                    LoginPage login = new LoginPage();
                    login.Show();
                    this.Close();
            }
        }

        private async void QuickAddBtn_Click(object sender, RoutedEventArgs e)
        {
            // 1. Get the text from the SearchBox
            string query = SearchBox.Text;

            // Simple validation to ensure they didn't just click with placeholder text
            if (string.IsNullOrWhiteSpace(query) || query == "Search for albums or artists...")
            {
                MessageBox.Show("Please enter an artist or album name first.");
                return;
            }

            try
            {
                // 2. Search Discogs and take the first result
                var results = await _discogsService.SearchAlbumsAsync(query);
                var bestMatch = results.FirstOrDefault();

                if (bestMatch != null)
                {
                    // 3. Fetch full details (Tracklist and Prices) using the ID
                    var fullRecord = await _discogsService.GetFullReleaseDetailsAsync(bestMatch.Id);

                    if (fullRecord != null)
                    {
                        // 4. Get the current User ID from Firebase Auth
                        // Your AuthService needs a way to expose the UID
                        string userId = _auth.GetClient()?.User?.Uid;

                        if (!string.IsNullOrEmpty(userId))
                        {
                            // 5. Save to Firebase
                            await _firebaseService.AddVinylAsync(fullRecord, userId);
                            MessageBox.Show($"Successfully added: {fullRecord.Artist} - {fullRecord.Album}!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No match found on Discogs.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var authService = new AuthService();
            authService.LogOut();

            LoginPage loginWindow = new LoginPage();
            loginWindow.Show();

            this.Close();
            //throw new NotImplementedException();
        }
    }

}
