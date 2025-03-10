using Microsoft.AspNetCore.Routing;

namespace SimpleInvoice.Features.Abstract;

public interface IEndPoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}