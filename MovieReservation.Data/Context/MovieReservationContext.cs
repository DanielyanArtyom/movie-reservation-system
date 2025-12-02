namespace MovieReservation.Data.Context;

public class MovieReservationContext: DbContext
{
    public MovieReservationContext(DbContextOptions<MovieReservationContext> options): base(options) {}

    public DbSet<Movie> Movies { get; set; } = default!;
    public DbSet<MovieGenre> Genres { get; set; } = default!;
    public DbSet<Room> Rooms { get; set; } = default!;
    public DbSet<Session> Sessions { get; set; } = default!;
    public DbSet<Ticket> Tickets { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<UserRole> UserRoles { get; set; } = default!;
    public DbSet<Permission> Permissions { get; set; } = default!;
    public DbSet<Role> Roles { get; set; } = default!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieReservationContext).Assembly);
    }
}