using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SimpleInvoice.Features.Abstract;
using System.Reflection;

namespace SimpleInvoice.Features
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddFeatures(this IServiceCollection services)
        {
            // Add end points
            services.AddEndPoints(Assembly.GetExecutingAssembly());
            // Add MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DepedencyInjection).Assembly));

            return services;
        }

        public static IServiceCollection AddEndPoints(this IServiceCollection services, Assembly assembly)
        {
            ServiceDescriptor[] serviceDescriptors = assembly
                .DefinedTypes
                .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                               type.IsAssignableTo(typeof(IEndPoint)))
                .Select(type => ServiceDescriptor.Transient(typeof(IEndPoint), type))
                .ToArray();

            services.TryAddEnumerable(serviceDescriptors);
            return services;
        }

        public static IApplicationBuilder MapEndpoints(this WebApplication app, RouteGroupBuilder? routeGroupBuilder = null)
        {
            IEnumerable<IEndPoint> endpoints = app.Services.GetRequiredService<IEnumerable<IEndPoint>>();

            IEndpointRouteBuilder builder = routeGroupBuilder is null ? app : routeGroupBuilder;

            foreach (IEndPoint endpoint in endpoints)
            {
                endpoint.MapEndpoint(builder);
            }

            return app;
        }
    }
}