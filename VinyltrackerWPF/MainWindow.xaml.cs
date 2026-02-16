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
using System.ComponentModel;

namespace VinyltrackerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        AuthService _auth = new AuthService();
        FirebaseService _firebaseService = new FirebaseService();
        DiscogsService _discogsService = new DiscogsService();

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
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
        
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int AlbumCounter
        {
            get
            {
                return _firebaseService.GetUserVinylsAsync(_auth.GetClient()?.User?.Uid ?? "").Result.Count;
                // This should ideally fetch the count from Firebase for the current user
                // For demonstration, we'll return a placeholder value
                //return 42; // Replace with actual count retrieval logic
            }
        }
        private async void QuickAddBtn_Click(object sender, RoutedEventArgs e)
        {
            string query = SearchBox.Text;
            
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

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {

        }
    }

}
