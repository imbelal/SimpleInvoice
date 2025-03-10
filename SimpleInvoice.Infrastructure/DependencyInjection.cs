using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleInvoice.Features.Abstract;
using SimpleInvoice.Infrastructure.Database;

namespace SimpleInvoice.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            // Register DbContext as scoped (recommended for DbContext)
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Database")));

            // Register the IApplicationDbContext interface to use ApplicationDbContext
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        }
    }
}
