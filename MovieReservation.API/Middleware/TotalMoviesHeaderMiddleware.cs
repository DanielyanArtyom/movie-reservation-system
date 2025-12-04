using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MovieReservation.API.Options;

namespace MovieReservation.API.Middleware;

public class TotalMoviesHeaderMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IMemoryCache _cache;
    private readonly AppSettingOptions  _options;

    public TotalMoviesHeaderMiddleware(RequestDelegate next, IMemoryCache cache, IOptions<AppSettingOptions> options)
    {
        _next = next;
        _cache = cache;
        _options = options.Value;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // var movieServiceService = context.RequestServices.GetRequiredService<IMovieService>();
        //
        // var totalMovies = await GetTotalGamesAsync(movieServiceService);
        //
        // context.Response.OnStarting(() =>
        // {
        //     context.Response.Headers.Append("x-total-numbers-of-games", totalMovies.ToString());
        //     return Task.CompletedTask;
        // });

        await _next(context);
    }

    // private async Task<int> GetTotalGamesAsync(IMovieService movieService)
    // {
    //     if (!_cache.TryGetValue("TotalMovies", out int totalGames))
    //     {
    //         totalGames = await movieService.GetGamesCountAsync();
    //         _cache.Set("TotalMovies", totalGames, TimeSpan.FromMinutes(_options.CachingTimeByMinutes));
    //     }
    //
    //     return totalGames;
    // }
}