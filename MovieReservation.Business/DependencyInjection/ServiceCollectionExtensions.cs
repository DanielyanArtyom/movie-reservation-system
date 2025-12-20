namespace MovieReservation.Business.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMovieReservationServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IPredefinedDataSetsProvider, PredefinedDataSetsProvider>();
        services.AddScoped<IGenreService, GenreService>();
        
        return services;
    }
}