using Microsoft.EntityFrameworkCore;
using SimpleInvoice.Features;
using SimpleInvoice.Infrastructure;
using SimpleInvoice.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddFeatures();
builder.Services.AddPersistence(builder.Configuration);

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapEndpoints();

if (!app.Environment.IsDevelopment())
{
    // Run auto migration for production and staging environment.
    RunAutoDatabaseMigration(app);
}

app.Run();
return;

void RunAutoDatabaseMigration(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ApplicationDbContext>();

        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }
}
