using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using MovieReservation.API.Mapping;
using Serilog;

namespace MovieReservation.API.Extensions;

public static class WebApplicationBuilderExtensions
{
    // public static WebApplicationBuilder Configure(this WebApplicationBuilder builder)
    // {
    //     builder.Configuration.AddJsonFile("appsettings.json", true)
    //         .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true)
    //         .AddEnvironmentVariables();
    //     
    //     builder.Services.Configure<AppSettingOptions>(builder.Configuration);
    //
    //     return builder;
    // }
    
    public static WebApplicationBuilder AddLogger(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File( 
                path: "Logs/log-.txt",
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message} {Properties}{NewLine}{Exception}",
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
            .CreateLogger();
        
        builder.Services.AddSerilog();

        return builder;
    }
    
    public static WebApplicationBuilder AddWebApi(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        
        builder.Services.AddEndpointsApiExplorer();
        
        builder.Services.AddHealthChecks();

        return builder;
    }
    
    public static WebApplicationBuilder AddMapper(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(DTOtoModelMapping).Assembly);

        return builder;
    }
    
    public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
    {
        if (builder.Environment.IsDevelopment() || builder.Environment.IsEnvironment("dev"))
        {
            builder.Services.AddSwaggerGen();
        }

        return builder;
    }
}