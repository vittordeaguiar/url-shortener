using Microsoft.EntityFrameworkCore;
using UrlShortener.Infrastructure.Data;
using UrlShortener.Infrastructure.Repositories;
using UrlShortener.Domain.Interfaces;
using UrlShortener.Application.Services;
using UrlShortener.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? "Host=db;Port=5432;Database=urlshortener;Username=postgres;Password=postgres";

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddScoped<IUrlRepository, UrlRepository>();
builder.Services.AddScoped<UrlShorteningService>();
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao aplicar migrações: {ex.Message}");
    }
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// app.UseHttpsRedirection();

app.MapUrlEndpoints();

app.Run();