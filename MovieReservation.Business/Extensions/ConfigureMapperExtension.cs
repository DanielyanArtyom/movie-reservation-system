

namespace MovieReservation.Business.Extensions;

public static class ConfigureMapperExtension
{
    public static IServiceCollection ConfigureMapper(this IServiceCollection builder)
    {
        builder.AddAutoMapper(typeof(EntitiesToModelMapping).Assembly);

        return builder;
    }
}