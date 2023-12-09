namespace ChristopherChurch.Data.DbAccess
{
    public interface ISqlDataAccess
    {
        string ConnectionStringName { get; set; }

        //Task<int> DeleteEntity(string sql, int id);
        //Task<T> GetEntityById<T>(string sql, int id);
        //Task<int> InsertEntity<T>(string sql, T entity);
        Task<List<T>> LoadData<T, U>(string sql, U parameters);
        Task SaveData<T>(string sql, T parameters);
        //Task<int> UpdateEntity<T>(string sql, T entity);
    }
}