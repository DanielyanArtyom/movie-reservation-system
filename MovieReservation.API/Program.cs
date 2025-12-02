
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddMemoryCache();

var sqlConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services
    .AddMovieReservationServices()
    .AddMovieReservationProvider(sqlConnectionString)
    .ConfigureMapper()
    .Configure<RouteOptions>(options => options.LowercaseUrls = true);

builder
    //.Configure()
    .AddLogger()
    .AddWebApi()
    .AddMapper()
    .AddSwagger();
   // .AddAuthentication();

builder.Services.AddEndpointsApiExplorer();   
builder.Services.AddControllers();

var app = builder.Build();

app.UseSerilogRequestLogging(); 

app.UseCors("AllowAll");

app.UseMiddlewares();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie Reservation API v1");
    options.RoutePrefix = string.Empty; // serves Swagger at root '/'
});
}

app.MapHealthChecks("/health");

app.UseDatabaseMigration();

app.MapControllers();

app.Run();