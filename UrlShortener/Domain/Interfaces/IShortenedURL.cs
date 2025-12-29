using UrlShortener.Domain.Entities;

namespace UrlShortener.Domain.Interfaces;

public interface IUrlRepository
{
    Task<ShortenedUrl?> GetByCodeAsync(string code);
    Task<ShortenedUrl?> GetByOriginalUrlAsync(string longUrl); // Para evitar duplicatas
    Task AddAsync(ShortenedUrl shortenedUrl);
    Task SaveChangesAsync();
}