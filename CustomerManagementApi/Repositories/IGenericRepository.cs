using CustomerManagementApi.Models;

namespace CustomerManagementApi.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllRecords();
        Task<T> GetRecordById(int id);
        Task AddRecord(T customer);
        Task UpdateRecord(T updatedCustomer);
        Task DeleteRecord(int id);
    }
}
