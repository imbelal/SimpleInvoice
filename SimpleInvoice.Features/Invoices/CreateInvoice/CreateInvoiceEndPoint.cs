using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SimpleInvoice.Features.Abstract;

namespace SimpleInvoice.Features.Invoices.CreateInvoice
{
    public class CreateInvoiceEndPoint : IEndPoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("api/v1/invoices", Handle);
        }

        private static async Task<Guid> Handle(
            [FromBody] CreateInvoiceCommand command,
            IMediator mediator,
            CancellationToken cancellation
        )
        {
            Guid invoiceId = await mediator.Send(command, cancellation);
            return invoiceId;
        }
    }
}
