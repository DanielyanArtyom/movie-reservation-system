using Microsoft.EntityFrameworkCore;
using MovieReservation.Data.Context;

namespace MovieReservation.Data.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMovieReservationProvider(this IServiceCollection services, string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentNullException(nameof(connectionString));
        }
        
        services.AddDbContext<MovieReservationContext>(options => options.UseNpgsql(connectionString));

        return services;
    }
}