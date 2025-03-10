using Microsoft.EntityFrameworkCore;
using SimpleInvoice.Domain.Invoices;
using SimpleInvoice.Features.Abstract;
using SimpleInvoice.Infrastructure.Database.EntityConfigurations;
using System.Reflection;

namespace SimpleInvoice.Infrastructure.Database
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : DbContext(options), IApplicationDbContext
    {
        public DbSet<T> GetDbSet<T>() where T : class
        {
            return Set<T>();
        }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add all entity configurations
            Assembly assemblyWithConfigurations = typeof(InvoiceItemConfiguration).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assemblyWithConfigurations);
            base.OnModelCreating(modelBuilder);
        }
    }
}
