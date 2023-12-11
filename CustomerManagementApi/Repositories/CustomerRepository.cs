using CustomerManagementApi.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CustomerManagementApi.Repositories
{
    public class CustomerRepository: GenericRepository<Customer>, ICustomerRepository
    {
       
        public CustomerRepository(AppDbContext context):base(context)
        {
            
        }

        
    }
}
