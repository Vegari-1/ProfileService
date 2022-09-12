using Npgsql;
using ProfileService.Model;
using ProfileService.Repository;
using System;

namespace ProfileService.IntegrationTests
{
    public static class DbExtensions
    {

        public static long CountTableRows(this IntegrationWebApplicationFactory<Program, AppDbContext> factory,
            string schemaName, string tableName)
        {
            long totalRows = -1;
            using (var connection = new NpgsqlConnection(factory.postgresContainer.ConnectionString))
            {
                using (var command = new NpgsqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT COUNT(*) FROM " + schemaName + ".\"" + tableName + "\"";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            totalRows = (long)reader[0];
                        }
                    }
                }
            }
            return totalRows;
        }

        public static void Insert(this IntegrationWebApplicationFactory<Program, AppDbContext> factory,
            string schemaName, string tableName, Profile profile)
        {
            string insertQuery = "INSERT INTO " + schemaName + ".\"" + tableName + 
                                 "\" (\"Id\", \"Public\", " +
                                 "\"Name\", \"Surname\", \"Username\", \"Email\", " +
                                 "\"Phone\", \"Gender\", \"DateOfBirth\", \"Biography\") " +
                                 "VALUES (@Id, @Public, @Name, @Surname, @Username, @Email, " +
                                 "@Phone, @Gender, @DateOfBirth, @Biography)";
            using (var connection = new NpgsqlConnection(factory.postgresContainer.ConnectionString))
            {
                using (var command = new NpgsqlCommand(insertQuery, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@Id", profile.Id);
                    command.Parameters.AddWithValue("@Public", profile.Public);
                    command.Parameters.AddWithValue("@Name", profile.Name);
                    command.Parameters.AddWithValue("@Surname", profile.Surname);
                    command.Parameters.AddWithValue("@Username", profile.Username);
                    command.Parameters.AddWithValue("@Email", profile.Email);
                    command.Parameters.AddWithValue("@Phone", profile.Phone);
                    command.Parameters.AddWithValue("@Gender", profile.Gender);
                    command.Parameters.AddWithValue("@DateOfBirth", profile.DateOfBirth);
                    command.Parameters.AddWithValue("@Biography", profile.Biography);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteById(this IntegrationWebApplicationFactory<Program, AppDbContext> factory,
            string schemaName, string tableName, Guid id)
        {
            using (var connection = new NpgsqlConnection(factory.postgresContainer.ConnectionString))
            {
                using (var command = new NpgsqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "DELETE FROM " + schemaName + ".\"" + tableName + "\" WHERE \"Id\" = @id";
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
