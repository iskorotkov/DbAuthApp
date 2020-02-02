using Npgsql;

namespace DbAuthApp.Registration
{
    public class IsLoginPresentCommand
    {
        private readonly NpgsqlCommand _command;

        public IsLoginPresentCommand(NpgsqlConnection connection, string login)
        {
            _command = new NpgsqlCommand
            {
                Connection = connection,
                CommandText = @"SELECT COUNT(*)
                                FROM users
                                WHERE users.login = @login"
            };

            _command.Parameters.AddWithValue("@login", login);
        }

        public bool Execute() => (long) _command.ExecuteScalar() > 0;
    }
}
