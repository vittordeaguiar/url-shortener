namespace UrlShortener.Domain.Entities;

public class ShortenedUrl
{
    public int Id { get; set; }
    public string LongUrl { get; set; } = string.Empty;
    public string ShortCode { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ShortenedUrl()
    {
    }

    public ShortenedUrl(string longUrl, string shortCode)
    {
        LongUrl = longUrl;
        ShortCode = shortCode;
        CreatedAt = DateTime.UtcNow;
    }
}