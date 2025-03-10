namespace SimpleInvoice.Domain.Invoices
{
    public class Invoice : BaseEntity
    {
        public string Number { get; private set; }
        public string Header { get; set; }
        public string Note { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public string Currency { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }

        public Invoice() { }

        public Invoice(int slNo, string header, string note, decimal tax, decimal discount) : base(Guid.NewGuid())
        {
            Number = $"INV-{DateTime.UtcNow.Year.ToString()}-{DateTime.UtcNow.Month.ToString()}-{slNo.ToString()}";
            Header = header;
            Note = note;
            Tax = tax;
            Discount = discount;
            Currency = "Euro";
        }

        public void AddItems(List<InvoiceItem> invoiceItems)
        {
            InvoiceItems = invoiceItems;
            SubTotal = invoiceItems.Sum(item => item.Total);
            Total = CalculateTotal(invoiceItems);
        }

        private decimal CalculateTotal(List<InvoiceItem> invoiceItems)
        {
            decimal itemsTotal = invoiceItems.Sum(item => item.Total);
            itemsTotal = itemsTotal + (itemsTotal * ((decimal)Tax / 100));
            itemsTotal = ((decimal)(100 - Discount) / 100) * itemsTotal;
            return itemsTotal;
        }
    }
}
