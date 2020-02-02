using Npgsql;

namespace DbAuthApp.Registration
{
    public class CountLoginCommand
    {
        private readonly NpgsqlCommand _command;

        public CountLoginCommand(NpgsqlConnection connection, string login)
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

        public long Execute() => (long) _command.ExecuteScalar();
    }
}
