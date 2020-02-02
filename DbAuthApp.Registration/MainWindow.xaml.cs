﻿using System;
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

        public MainWindow()
        {
            InitializeComponent();
            _loginDecorator = new TextBoxDecorator(LoginBox);
            _passwordDecorator = new TextBoxDecorator(PasswordBox);
            UpdateSignUpButtonState();
        }

        private void UpdateSignUpButtonState()
        {
            SignUpButton.IsEnabled = _loginDecorator.IsCorrect && _passwordDecorator.IsCorrect;
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            var login = RetrieveLogin();
            var salt = _saltGenerator.Next();
            var password = _hasher.Hash(RetrievePassword(), salt);

            string message;
            string caption;
            using (var connection = new NpgsqlConnection(BuildConnectionString()))
            {
                connection.Open();
                var command = new AddUserCommand(connection, login, password, salt);
                try
                {
                    command.Execute();
                    message = "You have successfully signed up";
                    caption = "Success!";
                }
                catch (Exception)
                {
                    message = "Can't create user with this login. Login violates database restrictions";
                    caption = "Failure!";
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
            else
            {
                _loginDecorator.InputIsIncorrect("Can't create user with this login");
            }

            UpdateSignUpButtonState();
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

        private bool ShouldClearFormAfterSignUp() =>
            ClearAfterSignUpCB.IsChecked != null && (bool) ClearAfterSignUpCB.IsChecked;

        private string RetrieveLogin() => _loginProcessor.RemoveWhitespaces(LoginBox.Text);

        private void LoginBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!LoginBox.Text.Any())
            {
                _loginDecorator.Reset();
                return;
            }

            var login = RetrieveLogin();
            if (_loginChecker.IsCorrect(login))
            {
                using var connection = new NpgsqlConnection(BuildConnectionString());
                connection.Open();
                if (new CountLoginCommand(connection, login).Execute() == 0)
                {
                    _loginDecorator.InputIsCorrect();
                }
                else
                {
                    _loginDecorator.InputIsIncorrect("There is another user with the same login");
                }
            }
            else
            {
                // TODO: Add more info about login
                _loginDecorator.InputIsIncorrect("Login is incorrect");
            }

            UpdateSignUpButtonState();
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

            UpdateSignUpButtonState();
        }

        private string RetrievePassword() => PasswordBox.Password.Trim();
    }
}
