using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using static Dapper.SqlMapper;

namespace ChristopherChurch.Data.DbAccess
{
    public class NpgsqlDataAccess : INpgsqlDataAccess
    {
        //private readonly string _connectionString;
        private readonly IConfiguration _config;

        public string ConnectionStringName { get; set; } = "Default";

        public NpgsqlDataAccess(IConfiguration config) //(string connectionString)
        {
            //_connectionString = connectionString;
            _config = config;
        }

        //  public IDbConnection Connection => new NpgsqlConnection(_connectionString);

        /* public async Task<IEnumerable<T>> GetAllAsync<T, U>(string sql, T parameters)
         {
             string connectionString = _config.GetConnectionString(ConnectionStringName);
             using (IDbConnection connection = new NpgsqlConnection(connectionString))
             {
                 var data = await connection.QueryAsync<T>(sql, parameters);
                 return data.ToList();
             }
         }*/
        /*public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var entities = await _config.QueryAsync<T>("SELECT * FROM " + typeof(T).Name);
            return entities.ToList();
        }*/

        public async Task<List<T>> LoadData<T>(string tableName)
        {
            string sql = $"SELECT * FROM {tableName};";

            string? connectionString = _config.GetConnectionString(ConnectionStringName);
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    var data = await connection.QueryAsync<T>(sql);

                    //var data = await connection.QueryAsync<T>(sql, parameters);
                    return data.ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error executing query: {ex.Message}");
                    throw;
                }
            }
        }

        public async Task<T> GetByIdAsync<T>(string tableName, int id)
        {
            string connectionString = _config.GetConnectionString(ConnectionStringName);
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var query = $"SELECT * FROM {tableName} WHERE Id = @Id";
                return await connection.QueryFirstOrDefaultAsync<T>(query, new { Id = id });
            }
        }

        public async Task<int> InsertAsync<T>(string tableName, T entity)
        {
            try
            {
                string connectionString = _config.GetConnectionString(ConnectionStringName);

                using (IDbConnection connection = new NpgsqlConnection(connectionString))
                {
                    // Use Dapper's built-in parameterization to avoid SQL injection
                    var query = $"INSERT INTO {tableName} VALUES @{GetParamNames(entity)}";

                    // ExecuteAsync takes an anonymous type or a dictionary as parameters
                    return await connection.ExecuteAsync(query, entity);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions or log them appropriately
                Console.WriteLine($"Error in InsertAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<int> UpdateAsync<T>(string tableName, T entity)
        {
            string connectionString = _config.GetConnectionString(ConnectionStringName);
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var query = $"UPDATE {tableName} SET {UpdateQuery(entity)} WHERE Id = @Id";
                return await connection.ExecuteAsync(query, entity);
            }
        }

        public async Task<int> DeleteAsync(string tableName, int id)
        {
            string connectionString = _config.GetConnectionString(ConnectionStringName);
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var query = $"DELETE FROM {tableName} WHERE Id = @Id";
                return await connection.ExecuteAsync(query, new { Id = id });
            }
        }
        private string GetParamNames<T>(T entity)
        {
            var propertyNames = GetPropertyNames(entity);

            return string.Join(",@", propertyNames);
        }
        private IEnumerable<string> GetPropertyNames(object entity)
        {
            return entity.GetType().GetProperties().Select(property => property.Name);
        }

        private string UpdateQuery(object entity)
        {
            var propertyNames = GetPropertyNames(entity);
            return string.Join(", ", propertyNames.Select(name => $"{name} = @{name}"));
        }
    }

}

