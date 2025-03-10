using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleInvoice.Domain.Invoices;

namespace SimpleInvoice.Infrastructure.Database.EntityConfigurations
{
    internal class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasColumnName("Title").HasColumnType("nvarchar(256)").IsRequired();
            builder.Property(x => x.UnitPrice).HasColumnName("UnitPrice").HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.Quantity).HasColumnName("Quantity").HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.Total).HasColumnName("Total").HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.Description).HasColumnName("Description").HasColumnType("nvarchar(512)").IsRequired(false);
            builder.Property(x => x.InvoiceId).HasColumnName("InvoiceId").HasColumnType("uniqueidentifier").IsRequired();
            builder.HasOne<Invoice>(x => x.Invoice).WithMany(x => x.InvoiceItems).HasForeignKey(x => x.InvoiceId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
