namespace MovieReservation.Business.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMovieReservationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IJwtService, JwtService>();
        
        return services;
    }
}