using CustomerManagementApi.Repositories;
using CustomerManagementApi.Models;

namespace CustomerManagementApi.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> GetCustomerById(int id);
        Task AddCustomer(Customer customer);
        Task UpdateCustomer(Customer updatedCustomer);
        Task DeleteCustomer(int id);
    }
}
