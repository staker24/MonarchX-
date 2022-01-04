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
        public override int SaveChanges()
        {
            AuditEntity();

            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var sb = new StringBuilder("DbUpdateException occurred. ");

                if (ex.InnerException is SqlException sqlException)
                {
                    sb.AppendLine($"SqlException - {sqlException.Message}. ");
                }
                else
                {
                    sb.AppendLine($"Details - {ex.InnerException?.Message}. ");
                }

                foreach (var eve in ex.Entries)
                {
                    sb.AppendLine($"Entity of type {eve.Entity.GetType().Name} in state {eve.State} could not be updated.");
                }

                throw new Exception(sb.ToString());
            }
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AuditEntity();

            try
            {
                var result = await base.SaveChangesAsync(true, cancellationToken);

                return result;
            }
            catch (DbUpdateException ex)
            {
                var sb = new StringBuilder("DbUpdateException occurred. ");

                if (ex.InnerException is SqlException sqlException)
                {
                    sb.AppendLine($"SqlException - {sqlException.Message}. ");
                }
                else
                {
                    sb.AppendLine($"Details - {ex.InnerException?.Message}. ");
                }

                foreach (var eve in ex.Entries)
                {
                    sb.AppendLine($"Entity of type {eve.Entity.GetType().Name} in state {eve.State} could not be updated.");
                }

                throw new Exception(sb.ToString());
            }
        }
        private void AuditEntity()
        {
            var now = DateTime.UtcNow;

            var userObjectId = _httpContextAccessor?.HttpContext.User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value ?? "Unknown";

            var entities = ChangeTracker.Entries().Where(x => x.Entity is IAuditedEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var e in entities)
            {
                var modifiedBy = userObjectId;

                if (e.State == EntityState.Modified)
                {
                    ((IAuditedEntity)e.Entity).ModifiedBy = modifiedBy;
                    ((IAuditedEntity)e.Entity).ModifiedDate = now;
                }
                else
                {
                    ((IAuditedEntity)e.Entity).CreatedBy = modifiedBy;
                    ((IAuditedEntity)e.Entity).CreatedDate = now;
                    ((IAuditedEntity)e.Entity).ModifiedBy = modifiedBy;
                    ((IAuditedEntity)e.Entity).ModifiedDate = now;
                }
            }
        }
    }
}