using MovieReservation.Data.Repository;

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
        
        services.AddScoped<IRepository<Guid, Movie>, BaseRepository<Movie>>();
        services.AddScoped<IRepository<Guid, MovieGenre>, BaseRepository<MovieGenre>>();
        services.AddScoped<IRepository<Guid, Room>, BaseRepository<Room>>();
        services.AddScoped<IRepository<Guid, Session>, BaseRepository<Session>>();
        services.AddScoped<IRepository<Guid, Ticket>, BaseRepository<Ticket>>();
        services.AddScoped<IRepository<Guid, User>, BaseRepository<User>>();
        services.AddScoped<IRepository<Guid, Role>, BaseRepository<Role>>();
        services.AddScoped<IRepository<Guid, Permission>, BaseRepository<Permission>>();
        services.AddScoped<IRepository<Guid, UserRole>, BaseRepository<UserRole>>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}