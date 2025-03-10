using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleInvoice.Domain.Invoices;
using SimpleInvoice.Features.Abstract;

namespace SimpleInvoice.Features.Invoices.CreateInvoice
{
    internal sealed record CreateInvoiceCommand : IRequest<Guid>
    {
        public string Header { get; set; }
        public string Note { get; set; }
        public List<InvoiceItemDto> Items { get; set; }
    }

    internal sealed class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateInvoiceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            int slNo = await _context.Invoices.CountAsync(cancellationToken) + 1;

            Invoice invoice = new(slNo, request.Header, request.Note, tax: 20, discount: 0);

            List<InvoiceItem> items = request.Items.Select(i =>
                new InvoiceItem(i.Title, i.Description, i.UnitPrice, i.Quantity, invoice.Id)).ToList();

            invoice.AddItems(items);

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync(cancellationToken);

            return invoice.Id;
        }
    }

    internal sealed record InvoiceItemDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
    }
}
