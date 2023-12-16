namespace ChristopherChurch.Data.DbAccess
{
    public interface INpgsqlDataAccess
    {
        string ConnectionStringName { get; set; }

        Task<int> DeleteAsync(string tableName, int id);
        Task<T> GetByIdAsync<T>(string tableName, int id);
        Task<int> InsertAsync<T>(string tableName, T entity);
        Task<List<T>> LoadData<T>(string tableName);
        Task<int> UpdateAsync<T>(string tableName, T entity);
    }
}