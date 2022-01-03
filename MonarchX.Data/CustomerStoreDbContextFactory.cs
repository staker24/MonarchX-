using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MonarchX.Data
{
    public class CustomerStoreDbContextFactory : IDesignTimeDbContextFactory<CustomerStoreDbContext>
    {
        public CustomerStoreDbContext CreateDbContext(string[] args)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true)
                .AddJsonFile("appsettings.Local.json", true)
                .AddEnvironmentVariables();

            var config = configBuilder.Build();

            string connectionString = config.GetConnectionString("BillingStoreDb");

            Console.Write($"Connection string: {connectionString}");

            var optionsBuilder = new DbContextOptionsBuilder<CustomerStoreDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new CustomerStoreDbContext(optionsBuilder.Options, null);
        }
    }
}
