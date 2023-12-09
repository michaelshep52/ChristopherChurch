namespace ChristopherChurch.Data.DbAccess
{
    public interface INpgsqlDataAccess
    {
        string ConnectionStringName { get; set; }

        Task<int> DeleteAsync(string tableName, int id);
        //Task<IEnumerable<T>> GetAllAsync<T, U>(string sql, T parameters);
        Task<IEnumerable<T>> GetAllAsync<T>(string tableName);
        Task<T> GetByIdAsync<T>(string tableName, int id);
        Task<int> InsertAsync<T>(string tableName, T entity);
        Task<int> UpdateAsync<T>(string tableName, T entity);
    }
}