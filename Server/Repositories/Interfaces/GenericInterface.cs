namespace News.Server.Repositories.Interfaces
{
    public interface GenericInterface<T> where T : class
    {
        IEnumerable<T> GetAllData(string value = "");

        T GetDataById(object id);

        T AddData(T value);

        T UpdateData(T value);

        void DeleteData(object id);
    }
}
