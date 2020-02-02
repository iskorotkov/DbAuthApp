using System.Collections.Generic;
using Npgsql;

namespace DbAuthApp.Registration
{
    public class AddUserCommand
    {
        private readonly NpgsqlCommand _command;
        
        public AddUserCommand(NpgsqlConnection connection, string login, IEnumerable<byte> password,
            IEnumerable<byte> salt)
        {
            _command = new NpgsqlCommand
            {
                CommandText = @"INSERT INTO users(login, password, salt, creation_date)
                                VALUES (@login, @password, @salt, current_date)",
                Connection = connection,
            };

            _command.Parameters.AddWithValue("@login", login);
            _command.Parameters.AddWithValue("@password", password);
            _command.Parameters.AddWithValue("@salt", salt);
        }

        public void Execute() => _command.ExecuteNonQuery();
    }
}
