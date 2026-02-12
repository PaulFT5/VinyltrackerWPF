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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        AuthService _auth = new AuthService();
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginStackPanel.IsEnabled = false;

            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                LoginStackPanel.IsEnabled = true;
                MessageBox.Show("Please enter both email and password.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                string token = await _auth.LoginAsync(email, password);

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

        public void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterPage register = new RegisterPage();
            register.Show();
            this.Close();
        }
    }

}
