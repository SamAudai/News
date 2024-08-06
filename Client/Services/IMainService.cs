namespace News.Client.Services
{
    public interface IMainService<T> where T : class
    {
        Task<List<T>> GetAll(string apiName);
        Task<T> GetData(string apiName);
        Task<T> AddData(T entity, string apiName);
        Task<T> UpdateData(T entity, string apiName);
        Task DeleteData(string apiName);
    }
}
