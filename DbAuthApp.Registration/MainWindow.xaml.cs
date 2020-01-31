using System.Data;
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
        private readonly SaltGenerator _saltGenerator = new SaltGenerator();
        private NpgsqlConnection _connection;
        private readonly LoginProcessor _loginProcessor = new LoginProcessor();
        private readonly LoginChecker _loginChecker = new LoginChecker();
        private readonly PasswordChecker _passwordChecker = new PasswordChecker();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e) => SignUpUser();

        private void OpenConnection()
        {
            // TODO: Open connection
            var connectionString = BuildConnectionString();
            _connection = new NpgsqlConnection(connectionString);
        }

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
            var login = _loginProcessor.RemoveWhitespaces(LoginBox.Text);
            var salt = _saltGenerator.Next();

            if (!_loginChecker.IsCorrect(login))
            {
                // TODO: Login isn't correct
                return;
            }

            var password = _hasher.Hash(PasswordBox.Password.Trim(), salt);
            if (!_passwordChecker.IsStrong(password, login))
            {
                // TODO: Password isn't correct
                return;
            }

            if (_connection.State == ConnectionState.Closed)
            {
                OpenConnection();
            }

            var command = BuildAddCommand(login, password, salt);
            command.ExecuteNonQuery();
        }

        private NpgsqlCommand BuildAddCommand(string username, string password, string salt)
        {
            var command = new NpgsqlCommand
            {
                CommandText = @"
INSERT INTO users(username, password, salt, creation_date)
VALUES (@username, @password, @salt, current_timestamp)",
                Connection = _connection
            };

            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@salt", salt);

            return command;
        }
    }
}
