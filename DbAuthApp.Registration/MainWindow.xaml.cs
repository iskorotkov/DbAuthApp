﻿using System.Linq;
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

        public MainWindow()
        {
            InitializeComponent();
            _loginDecorator = new TextBoxDecorator(LoginBox);
            _passwordDecorator = new TextBoxDecorator(PasswordBox);
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
            
            var connectionString = BuildConnectionString();
            // ReSharper disable once ConvertToUsingDeclaration
            using (var connection = new NpgsqlConnection(connectionString))
            {
                var command = BuildAddCommand(connection, login, password, salt);
                command.ExecuteNonQuery();
            }
        }

        private string RetrieveLogin()
        {
            return _loginProcessor.RemoveWhitespaces(LoginBox.Text);
        }

        private NpgsqlCommand BuildAddCommand(NpgsqlConnection connection, string username, string password,
            string salt)
        {
            var command = new NpgsqlCommand
            {
                CommandText = @"
INSERT INTO users(username, password, salt, creation_date)
VALUES (@username, @password, @salt, current_timestamp)",
                Connection = connection
            };

            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@salt", salt);

            return command;
        }

        private void LoginBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!LoginBox.Text.Any())
            {
                _loginDecorator.Reset();
                return;
            }

            if (_loginChecker.IsCorrect(RetrieveLogin()))
            {
                _loginDecorator.InputIsCorrect();
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
                _passwordDecorator.Reset();
                return;
            }

            if (_passwordChecker.IsStrong(RetrievePassword(), RetrieveLogin()))
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
