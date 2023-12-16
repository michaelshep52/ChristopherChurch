namespace ChristopherChurch.Data.DbAccess
{
    public interface ISqlDataAccess
    {
        string ConnectionStringName { get; set; }

        Task<List<T>> LoadData<T, U>(string sql, U parameters);
        Task<T> LoadSingleData<T, U>(string sql, U parameters);
        Task SaveData<T>(string sql, T parameters);
    }
}