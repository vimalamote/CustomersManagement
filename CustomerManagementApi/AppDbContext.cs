﻿using Microsoft.EntityFrameworkCore;
using CustomerManagementApi.Models;

namespace CustomerManagementApi
{
    public class AppDbContext: DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
