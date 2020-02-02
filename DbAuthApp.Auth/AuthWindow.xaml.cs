using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DbAuthApp.Login;
using DbAuthApp.Passwords;
using Npgsql;

namespace DbAuthApp.Auth
{
    public partial class AuthWindow
    {
        private readonly PasswordHasher _hasher = new PasswordHasher();
        private readonly LoginChecker _loginChecker = new LoginChecker();
        private readonly LoginProcessor _loginProcessor = new LoginProcessor();
        private readonly PasswordChecker _passwordChecker = new PasswordChecker();

        public AuthWindow()
        {
            InitializeComponent();
            UpdateAuthButtonState();
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            var login = RetrieveLogin();

            var message = "There is no user with this login and password";
            var caption = "Failure!";

            if (_loginChecker.IsCorrect(login) && _passwordChecker.IsStrong(RetrievePassword(), login))
            {
                using var connection = new NpgsqlConnection(BuildConnectionString());
                connection.Open();

                try
                {
                    var salt = new RetrieveSaltCommand(connection, login).Execute();
                    var password = _hasher.Hash(RetrievePassword(), salt);

                    var passwordMatches = new IsPasswordMatchesCommand(connection, login, password);
                    if (passwordMatches.Execute())
                    {
                        message = "You have successfully authenticated!";
                        caption = "Success!";
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            PostSignUp(message, caption);
        }

        private void PostSignUp(string message, string caption)
        {
            MessageBox.Show(this, message, caption, MessageBoxButton.OK);
            if (ShouldClearFormAfterSignUp())
            {
                ClearForm();
            }
        }

        private static string BuildConnectionString() =>
            new NpgsqlConnectionStringBuilder
            {
                Username = "postgres",
                Password = "1234",
                Database = "auth_app",
                Host = "localhost"
            }.ConnectionString;

        private void ClearForm()
        {
            LoginBox.Text = string.Empty;
            PasswordBox.Password = string.Empty;
        }

        private void UpdateAuthButtonState()
        {
            AuthButton.IsEnabled = RetrieveLogin().Any() && RetrievePassword().Any();
        }

        private bool ShouldClearFormAfterSignUp() =>
            ClearAfterAuthCb.IsChecked != null && (bool) ClearAfterAuthCb.IsChecked;

        private string RetrieveLogin() => _loginProcessor.RemoveWhitespaces(LoginBox.Text);

        private string RetrievePassword() => PasswordBox.Password.Trim();

        private void LoginBox_TextChanged(object sender, TextChangedEventArgs e) => UpdateAuthButtonState();

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e) => UpdateAuthButtonState();
    }
}
