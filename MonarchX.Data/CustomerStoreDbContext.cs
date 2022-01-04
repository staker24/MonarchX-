using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MonarchX.Data.Models;

namespace MonarchX.Data
{
    public class CustomerStoreDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Product> Products { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomerStoreDbContext(DbContextOptions<CustomerStoreDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;

            /*
            var connection = (SqlConnection)Database.GetDbConnection();
            if (connection?.ConnectionString.Contains("Password", System.StringComparison.CurrentCultureIgnoreCase) == false)
            {
                connection.AccessToken = new AzureServiceTokenProvider().GetAccessTokenAsync("https://database.windows.net/").Result;
            } */
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                        .HasIndex(e => e.CustomerId)
                        .IsUnique();

            modelBuilder.Entity<Product>()
                        .HasIndex(e => e.ItemNumber)
                        .IsUnique();
                         
           modelBuilder.Entity<Sale>()
                        .HasIndex(e=>e.InvoiceId)
                        .IsUnique();
                    
        }
        
    }

}