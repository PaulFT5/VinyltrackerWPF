using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VinylTrackerWPF.Services;

namespace VinyltrackerWPF.Page
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Window
    {
        AuthService _auth = new AuthService();
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string email = EmailTextBox.Text;
            string password = PasswordBoxRegister.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

            if (!PasswordsMatch(password, confirmPassword)) return;

            try
            {
                string token = await _auth.RegisterAsync(email, password, username);

                if (!string.IsNullOrEmpty(token))
                {
                    MainWindow main = new MainWindow();
                    main.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                LoginStackPanel.IsEnabled = true;
                if (ex.Message.Contains("INVALID_EMAIL"))
                {
                    MessageBox.Show("The email address is badly formatted.", "Login Error");
                }
                else if (ex.Message.Contains("INVALID_PASSWORD"))
                {
                    MessageBox.Show("Wrong password. Please try again.", "Login Error");
                }
                else
                {
                    MessageBox.Show("An unexpected error occurred. Check your internet connection.");
                }
            }
        }

        private bool PasswordsMatch(string password, string confirmPassword)
        {
            if(password == confirmPassword) return true;

            if(string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in both password fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            MessageBox.Show("Please try again.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
    }
}
