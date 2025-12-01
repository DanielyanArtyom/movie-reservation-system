using Microsoft.EntityFrameworkCore;

namespace MovieReservation.Data.Context;

public class MovieReservationContext: DbContext
{
    public MovieReservationContext(DbContextOptions<MovieReservationContext> options): base(options) {}
    
    //public DbSet<Game> Games { get; set; } = default!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieReservationContext).Assembly);
    }
}