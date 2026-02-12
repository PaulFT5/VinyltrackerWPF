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

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
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