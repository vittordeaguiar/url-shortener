using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Interfaces;

namespace UrlShortener.Application.Services;

public class UrlShorteningService(IUrlRepository repository)
{
    private const int CodeLength = 7;
    private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    public async Task<string> GenerateShortUrlAsync(string originalUrl)
    {
        if (!Uri.TryCreate(originalUrl, UriKind.Absolute, out _))
        {
            throw new ArgumentException("URL inválida.");
        }

        var existing = await repository.GetByOriginalUrlAsync(originalUrl);
        if (existing is not null)
        {
            return existing.ShortCode;
        }

        var code = GenerateCode();


        var shortened = new ShortenedUrl(originalUrl, code);
        await repository.AddAsync(shortened);
        await repository.SaveChangesAsync();

        return code;
    }

    public async Task<string?> GetOriginalUrlAsync(string code)
    {
        var shortened = await repository.GetByCodeAsync(code);
        return shortened?.LongUrl;
    }

    private static string GenerateCode()
    {
        var chars = new char[CodeLength];
        for (var i = 0; i < CodeLength; i++)
        {
            chars[i] = Alphabet[Random.Shared.Next(Alphabet.Length)];
        }

        return new string(chars);
    }
}