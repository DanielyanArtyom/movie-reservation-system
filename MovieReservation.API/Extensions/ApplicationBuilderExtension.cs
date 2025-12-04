namespace MovieReservation.API.Extensions;

public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var context = serviceScope.ServiceProvider.GetService<MovieReservationContext>()!;
        context.Database.Migrate();
        return app;
    }
    
    public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ExceptionMiddleware>();
        builder.UseMiddleware<SerilogMiddleware>();
        builder.UseMiddleware<TotalMoviesHeaderMiddleware>();

        return builder;
    }
}