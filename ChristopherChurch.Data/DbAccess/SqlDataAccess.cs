using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace ChristopherChurch.Data.DbAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public string ConnectionStringName { get; set; } = "Default";

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<T>> LoadData<T, U>(string sql, U parameters)
        {
            string? connectionString = _config.GetConnectionString(ConnectionStringName);
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var data = await connection.QueryAsync<T>(sql, parameters);
                return data.ToList();
            }
        }

        public async Task SaveData<T>(string sql, T parameters)
        {
            string? connectionString = _config.GetConnectionString(ConnectionStringName);
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql, parameters);
            }
        }
        /*
        public async Task<T> GetEntityById<T>(string sql, int id)
        {
            string? connectionString = _config.GetConnectionString(ConnectionStringName);
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<T>(sql, new { Id = id });
            }
        }

        public async Task<int> InsertEntity<T>(string sql, T entity)
        {
            try
            {
                string? connectionString = _config.GetConnectionString(ConnectionStringName);

                using (IDbConnection connection = new NpgsqlConnection(connectionString))
                {
                    // Use Dapper's built-in parameterization to avoid SQL injection
                    var query = $"{sql} RETURNING id";

                    // ExecuteAsync takes an anonymous type or a dictionary as parameters
                    return await connection.ExecuteScalarAsync<int>(query, entity);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions or log them appropriately
                Console.WriteLine($"Error in InsertEntity: {ex.Message}");
                throw;
            }
        }

        public async Task<int> UpdateEntity<T>(string sql, T entity)
        {
            string? connectionString = _config.GetConnectionString(ConnectionStringName);
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                return await connection.ExecuteAsync(sql, entity);
            }
        }

        public async Task<int> DeleteEntity(string sql, int id)
        {
            string? connectionString = _config.GetConnectionString(ConnectionStringName);
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                return await connection.ExecuteAsync(sql, new { Id = id });
            }
        }
        */
    }
}




