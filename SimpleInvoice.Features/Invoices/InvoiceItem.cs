namespace SimpleInvoice.Domain.Invoices
{
    public class InvoiceItem : BaseEntity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Quantity { get; private set; }
        public decimal Total { get; private set; }
        public Guid InvoiceId { get; private set; }
        public Invoice Invoice { get; private set; }

        public InvoiceItem()
        {

        }

        public InvoiceItem(string title, string description, decimal unitPrice, decimal quantity, Guid invoiceId) : base(Guid.NewGuid())
        {
            Title = title;
            Description = description;
            UnitPrice = unitPrice;
            Quantity = quantity;
            Total = unitPrice * quantity;
            InvoiceId = invoiceId;
        }
    }
}
