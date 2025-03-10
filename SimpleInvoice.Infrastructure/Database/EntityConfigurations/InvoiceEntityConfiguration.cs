using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleInvoice.Domain.Invoices;

namespace SimpleInvoice.Infrastructure.Database.EntityConfigurations
{
    public class InvoiceEntityConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Header).HasColumnName("Header").HasColumnType("nvarchar(256)").IsRequired();
            builder.Property(x => x.Note).HasColumnName("Note").HasColumnType("nvarchar(512)").IsRequired(false);
            builder.Property(x => x.SubTotal).HasColumnName("SubTotal").HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.Tax).HasColumnName("Tax").HasColumnType("decimal(5,2)").IsRequired();
            builder.Property(x => x.Discount).HasColumnName("Discount").HasColumnType("decimal(5,2)").IsRequired();
            builder.Property(x => x.Total).HasColumnName("Total").HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.Currency).HasColumnName("Currency").HasColumnType("nvarchar(10)").IsRequired();
            builder.Property(x => x.Number).HasColumnName("Number").HasColumnType("nvarchar(50)").IsRequired();
        }
    }
}
