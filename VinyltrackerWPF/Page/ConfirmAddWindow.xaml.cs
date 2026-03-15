using System.Windows;
using VinylTrackerWPF.Models;

namespace VinyltrackerWPF.Page
{
    public partial class ConfirmAddWindow : Window
    {
        public bool Confirmed { get; private set; } = false;

        public ConfirmAddWindow(VinylRecord record)
        {
            InitializeComponent();
            AlbumName.Text = record.Album;
            ArtistName.Text = record.Artist;
            YearText.Text = record.Year;
            AlbumImage.Source = new System.Windows.Media.Imaging.BitmapImage(
                new Uri(record.ImageUrl ?? ""));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Confirmed = true;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Confirmed = false;
            this.Close();
        }
    }
}