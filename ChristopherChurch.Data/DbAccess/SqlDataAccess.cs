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

            var dataSourceBuilder = new NpgsqlConnectionStringBuilder(connectionString);
            var dataSource = new NpgsqlConnection(dataSourceBuilder.ConnectionString);

            await dataSource.OpenAsync();
            try
            {
                var data = await dataSource.QueryAsync<T>(sql, parameters, commandType: CommandType.Text);
                return data.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing query: {ex.Message}");
                throw;
            }

        }

        public async Task<T> LoadSingleData<T, U>(string sql, U parameters)
        {
            string? connectionString = _config.GetConnectionString(ConnectionStringName);

            var dataSourceBuilder = new NpgsqlConnectionStringBuilder(connectionString);
            var dataSource = new NpgsqlConnection(dataSourceBuilder.ConnectionString);

            await dataSource.OpenAsync();
            try
            {
                var data = await dataSource.QueryFirstOrDefaultAsync<T>(sql, parameters, commandType: CommandType.Text);
                return data!;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing query: {ex.Message}");
                throw;
            }
        }


        public async Task SaveData<T>(string sql, T parameters)
        {
            string? connectionString = _config.GetConnectionString(ConnectionStringName);

            var dataSourceBuilder = new NpgsqlConnectionStringBuilder(connectionString);
            var dataSource = new NpgsqlConnection(dataSourceBuilder.ConnectionString);

            await dataSource.OpenAsync();
            try
            {
                await dataSource.ExecuteAsync(sql, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing query: {ex.Message}");
                throw;
            }
        }
    }
}
