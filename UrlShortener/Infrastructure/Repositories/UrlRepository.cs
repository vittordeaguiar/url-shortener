using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Interfaces;
using UrlShortener.Infrastructure.Data;

namespace UrlShortener.Infrastructure.Repositories;

public class UrlRepository(AppDbContext context) : IUrlRepository
{
    public async Task<ShortenedUrl?> GetByCodeAsync(string code) =>
        await context.ShortenedUrls.FirstOrDefaultAsync(s => s.ShortCode == code);

    public async Task<ShortenedUrl?> GetByOriginalUrlAsync(string longUrl) =>
        await context.ShortenedUrls.FirstOrDefaultAsync(s => s.LongUrl == longUrl);

    public async Task AddAsync(ShortenedUrl shortenedUrl) => await context.ShortenedUrls.AddAsync(shortenedUrl);

    public async Task SaveChangesAsync() => await context.SaveChangesAsync();
}