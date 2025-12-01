
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

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSerilogRequestLogging(); 

app.UseCors("AllowAll");

app.UseMiddlewares();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health");

app.UseDatabaseMigration();

app.MapControllers();

app.Run();