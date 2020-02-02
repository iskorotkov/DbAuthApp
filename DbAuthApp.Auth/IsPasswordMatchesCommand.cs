using Npgsql;

namespace DbAuthApp.Auth
{
    public class IsPasswordMatchesCommand
    {
        private readonly NpgsqlCommand _command;
        
        public IsPasswordMatchesCommand(NpgsqlConnection connection, string login, byte[] password)
        {
            _command = new NpgsqlCommand
            {
                Connection = connection,
                CommandText = @"SELECT COUNT(*)
                                FROM users
                                WHERE users.login = @login
                                  AND users.password = @password"
            };
            _command.Parameters.AddWithValue("@login", login);
            _command.Parameters.AddWithValue("@password", password);
        }

        public bool Execute() => (long) _command.ExecuteScalar() > 0;
    }
}
