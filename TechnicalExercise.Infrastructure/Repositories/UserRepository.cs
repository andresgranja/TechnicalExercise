using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalExercise.Core.Entities;

namespace TechnicalExercise.Infrastructure.Repositories
{
    public class UserRepository
    {
        private readonly string? connectionString;

        public UserRepository(string? connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            User? user = null;
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT UserId, Username, PasswordHash FROM Users WHERE Username = @Username AND PasswordHash = @PasswordHash", connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@PasswordHash", password); // Assuming password is stored as a hash

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        user = new User
                        {
                            UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            Username = reader.GetString(reader.GetOrdinal("Username")),
                            PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash"))
                        };
                    }
                }
            }

            return user;
        }

        public async Task CreateUserAsync(User user)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("INSERT INTO Users (Username, PasswordHash) VALUES (@Username, @PasswordHash)", connection);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);  // Assuming the password is hashed before this method is called

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
