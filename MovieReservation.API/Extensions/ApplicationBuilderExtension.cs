namespace MovieReservation.API.Extensions;

public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();

        var contextFactory = serviceScope.ServiceProvider.GetService<IDbContextFactory<MovieReservationContext>>()!;
        
        using (var context = contextFactory.CreateDbContext())
        {
            context.Database.EnsureCreated();
        }

        return app;
    }
    
    public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ExceptionMiddleware>();
        builder.UseMiddleware<SerilogMiddleware>();

        return builder;
    }
}