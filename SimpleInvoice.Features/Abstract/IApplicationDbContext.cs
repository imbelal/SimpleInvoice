using Microsoft.EntityFrameworkCore;
using SimpleInvoice.Domain.Invoices;

namespace SimpleInvoice.Features.Abstract
{
    public interface IApplicationDbContext : IDisposable
    {
        DbSet<T> GetDbSet<T>() where T : class;
        DbSet<Invoice> Invoices { get; set; }
        DbSet<InvoiceItem> InvoiceItems { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
