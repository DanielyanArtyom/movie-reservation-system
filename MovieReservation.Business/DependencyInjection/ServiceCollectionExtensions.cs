namespace MovieReservation.Business.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMovieReservationServices(this IServiceCollection services)
    {
       // services.AddSingleton<IFileService, FileService>();
        return services;
    }
}