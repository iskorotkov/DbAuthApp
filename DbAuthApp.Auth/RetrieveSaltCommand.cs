using Npgsql;

namespace DbAuthApp.Auth
{
    public class RetrieveSaltCommand
    {
        private readonly NpgsqlCommand _command;

        public RetrieveSaltCommand(NpgsqlConnection connection, string login)
        {
            _command = new NpgsqlCommand
            {
                Connection = connection,
                CommandText = @"SELECT users.salt
                                FROM users
                                WHERE users.login = @login"
            };
            _command.Parameters.AddWithValue("@login", login);
        }

        public byte[] Execute() => (byte[]) _command.ExecuteScalar();
    }
}
