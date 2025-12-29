using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ShortenedUrl> ShortenedUrls { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShortenedUrl>(builder =>
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.ShortCode).HasMaxLength(7).IsRequired();
            builder.HasIndex(s => s.ShortCode).IsUnique(); // Performance improve
            builder.Property(s => s.LongUrl).IsRequired();
        });
    }
}