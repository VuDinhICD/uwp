using System.Configuration;
using System.Threading.Tasks;
using Npgsql;
using System.Diagnostics;
using System.Data;

namespace universal_windows_platform_cs.Core.Services
{
    public static class DataSourceService
    {
        private static NpgsqlConnection connection;
        public static async Task<bool> TestConnection()
        {
            string connString;
            var connectionStrings = ConfigurationManager.ConnectionStrings;
            
            if (connectionStrings != null)
            {
                connString = connectionStrings["PostgreSQLConnectionString"].ToString();
            }
            else
            {
                return false;
            }

            try
            {
                connection = new NpgsqlConnection
                {
                    ConnectionString = connString
                };
                await connection.OpenAsync();
                NpgsqlCommand cmd = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = "SELECT '1'",
                    Connection = connection
                };

                using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        if (reader.GetString(0) == "1")
                        {
                            await connection.CloseAsync();
                            return true;
                        }
                    }
                }
                await connection.CloseAsync();
                return false;
            }
            catch
            {
                Debug.WriteLine("Please check server or config database!");
                await connection.CloseAsync();
                return false;
            }
        }

        public static async Task<NpgsqlDataReader> Query(string query)
        {
            string connString;
            var connectionStrings = ConfigurationManager.ConnectionStrings;

            if (connectionStrings != null)
            {
                connString = connectionStrings["PostgreSQLConnectionString"].ToString();
            }
            else
            {
                return null;
            }

            try
            {
                connection = new NpgsqlConnection
                {
                    ConnectionString = connString
                };
                await connection.OpenAsync();
                NpgsqlCommand cmd = new NpgsqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText = query,
                    Connection = connection
                };

                using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    await connection.CloseAsync();
                    return reader;
                }
            }
            catch
            {
                Debug.WriteLine("Please check server or config database!");
                await connection.CloseAsync();
                return null;
            }
        }
    }
}

