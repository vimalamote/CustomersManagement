using CustomerManagementApi.Models;
using CustomerManagementApi.Repositories;

namespace CustomerManagementApi.Services
{
    public class CustomerService:ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            this._customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _customerRepository.GetAllRecords();
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await _customerRepository.GetRecordById(id);
        }

        public async Task AddCustomer(Customer customer)
        {
            await _customerRepository.AddRecord(customer);
        }

        public async Task UpdateCustomer(Customer updatedCustomer)
        {
            await _customerRepository.UpdateRecord(updatedCustomer);
        }

        public async Task DeleteCustomer(int id)
        {
            await _customerRepository.DeleteRecord(id);
        }
    }
}
