using System;
using System.Collections.Generic;
using System.Data;
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

        public async Task<IEnumerable<T>> GetAllAsync<T, U>(string sql, T parameters)
        {
            string connectionString = _config.GetConnectionString(ConnectionStringName);
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var data = await connection.QueryAsync<T>(sql, parameters);
                return data.ToList();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(string tableName)
        {
            //using var connection = Connection;
            //connection.Open();
            string connectionString = _config.GetConnectionString(ConnectionStringName);
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var query = $"SELECT * FROM {tableName}";
                return await connection.QueryAsync<T>(query);
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
            string connectionString = _config.GetConnectionString(ConnectionStringName);
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var query = $"INSERT INTO {tableName} VALUES (@{string.Join(", @", GetPropertyNames(entity))})";
                return await connection.ExecuteAsync(query, entity);
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

