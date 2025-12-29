using UrlShortener.Application.DTOs;
using UrlShortener.Application.Services;

namespace UrlShortener.Endpoints;

public static class UrlEndpoints
{
    public static void MapUrlEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/shorten", async (ShortenUrlRequest request, UrlShorteningService service, HttpContext httpContext) =>
        {
            try
            {
                var code = await service.GenerateShortUrlAsync(request.Url);
                var scheme = httpContext.Request.Scheme;
                var host = httpContext.Request.Host;
                var shortUrl = $"{scheme}://{host}/{code}";

                return Results.Ok(new { code, shortUrl });
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        app.MapGet("/{code}", async (string code, UrlShorteningService service) =>
        {
            var originalUrl = await service.GetOriginalUrlAsync(code);

            return originalUrl is null
                ? Results.NotFound()
                : Results.Redirect(originalUrl);
        });
    }
}