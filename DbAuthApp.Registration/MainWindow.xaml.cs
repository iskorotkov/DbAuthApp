using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DbAuthApp.Login;
using DbAuthApp.Passwords;
using Npgsql;

namespace DbAuthApp.Registration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly PasswordHasher _hasher = new PasswordHasher();
        private readonly LoginChecker _loginChecker = new LoginChecker();
        private readonly TextBoxDecorator _loginDecorator;
        private readonly LoginProcessor _loginProcessor = new LoginProcessor();
        private readonly PasswordChecker _passwordChecker = new PasswordChecker();
        private readonly TextBoxDecorator _passwordDecorator;
        private readonly SaltGenerator _saltGenerator = new SaltGenerator();
        private bool _isLoginCorrect;
        private bool _isPasswordCorrect;

        public MainWindow()
        {
            InitializeComponent();
            _loginDecorator = new TextBoxDecorator(LoginBox);
            _passwordDecorator = new TextBoxDecorator(PasswordBox);
            UpdateSignUpButtonState();
        }

        private bool IsLoginCorrect
        {
            get => _isLoginCorrect;
            set
            {
                _isLoginCorrect = value;
                UpdateSignUpButtonState();
            }
        }

        private bool IsPasswordCorrect
        {
            get => _isPasswordCorrect;
            set
            {
                _isPasswordCorrect = value;
                UpdateSignUpButtonState();
            }
        }

        private void UpdateSignUpButtonState()
        {
            SignUpButton.IsEnabled = IsLoginCorrect && IsPasswordCorrect;
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e) => SignUpUser();

        private static string BuildConnectionString() =>
            new NpgsqlConnectionStringBuilder
            {
                Username = "postgres",
                Password = "1234",
                Database = "auth_app",
                Host = "localhost"
            }.ConnectionString;

        private void SignUpUser()
        {
            var login = RetrieveLogin();
            var salt = _saltGenerator.Next();
            var password = _hasher.Hash(RetrievePassword(), salt);

            // ReSharper disable once ConvertToUsingDeclaration
            using (var connection = new NpgsqlConnection(BuildConnectionString()))
            {
                connection.Open();
                var command = BuildAddCommand(connection, login, password, salt);
                string message;
                string caption;
                try
                {
                    command.ExecuteNonQuery();
                    message = "You have successfully signed up";
                    caption = "Success!";
                }
                catch (Exception)
                {
                    message = "Can't create user with this login. Login violates database restrictions";
                    caption = "Failure!";
                }

                MessageBox.Show(this, message, caption, MessageBoxButton.OK);
                if (ShouldClearFormAfterSignUp())
                {
                    ClearForm();
                }
                else
                {
                    _loginDecorator.InputIsIncorrect("Can't create user with this login");
                }

                IsLoginCorrect = false;
            }
        }

        private void ClearForm()
        {
            LoginBox.Text = string.Empty;
            PasswordBox.Password = string.Empty;
        }

        private bool ShouldClearFormAfterSignUp()
        {
            return ClearAfterSignUpCB.IsChecked != null && (bool) ClearAfterSignUpCB.IsChecked;
        }

        private string RetrieveLogin()
        {
            return _loginProcessor.RemoveWhitespaces(LoginBox.Text);
        }

        private NpgsqlCommand BuildAddCommand(NpgsqlConnection connection, string login, IEnumerable<byte> password,
            IEnumerable<byte> salt)
        {
            var command = new NpgsqlCommand
            {
                CommandText = @"
INSERT INTO users(login, password, salt, creation_date)
VALUES (@login, @password, @salt, current_timestamp)",
                Connection = connection,
            };

            command.Parameters.AddWithValue("@login", login);
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@salt", salt);
            return command;
        }

        private NpgsqlCommand BuildCountLoginCommand(NpgsqlConnection connection, string login)
        {
            var command = new NpgsqlCommand
            {
                Connection = connection,
                CommandText = @"
SELECT COUNT(*)
FROM users
WHERE users.login = @login",
            };

            command.Parameters.AddWithValue("@login", login);
            return command;
        }

        private void LoginBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!LoginBox.Text.Any())
            {
                IsLoginCorrect = false;
                _loginDecorator.Reset();
                return;
            }

            var login = RetrieveLogin();
            IsLoginCorrect = _loginChecker.IsCorrect(login);
            if (IsLoginCorrect)
            {
                // ReSharper disable once ConvertToUsingDeclaration
                using (var connection = new NpgsqlConnection(BuildConnectionString()))
                {
                    connection.Open();
                    var loginsPresent = (long) BuildCountLoginCommand(connection, login).ExecuteScalar();
                    IsLoginCorrect = loginsPresent == 0;
                    if (IsLoginCorrect)
                    {
                        _loginDecorator.InputIsCorrect();
                    }
                    else
                    {
                        _loginDecorator.InputIsIncorrect("There is another user with the same login");
                    }
                }
            }
            else
            {
                // TODO: Add more info about login
                _loginDecorator.InputIsIncorrect("Login is incorrect");
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!PasswordBox.Password.Any())
            {
                IsPasswordCorrect = false;
                _passwordDecorator.Reset();
                return;
            }

            IsPasswordCorrect = _passwordChecker.IsStrong(RetrievePassword(), RetrieveLogin());
            if (IsPasswordCorrect)
            {
                _passwordDecorator.InputIsCorrect();
            }
            else
            {
                // TODO: Show password strength indicator
                _passwordDecorator.InputIsIncorrect("Your password isn't strong enough");
            }
        }

        private string RetrievePassword() => PasswordBox.Password.Trim();
    }
}
