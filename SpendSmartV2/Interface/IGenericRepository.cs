namespace SpendSmartV2.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(string userId);
        Task<T> GetAsync(string userId, int id);
        Task<bool> AddEntity(T model);
        Task<bool> UpdateEntity(T model);
        Task<bool> DeleteEntity(int id);
    }
}