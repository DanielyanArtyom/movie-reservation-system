namespace MovieReservation.Data.Repository;

public class UnitOfWork: IUnitOfWork
{
    private readonly MovieReservationContext _context;
    
    public IRepository<Guid, Movie> Movies { get; }
    public IRepository<Guid, MovieGenre> MovieGenres { get; }
    public IRepository<Guid, Room> Rooms { get; }
    public IRepository<Guid, Session> Sessions { get; }
    public IRepository<Guid, Ticket> Tickets { get; }
    public IUserRepository Users { get; }
    public IRepository<Guid, Permission> Permissions { get; }
    public IRepository<Guid, UserRole> UserRoles { get; }
    public IRepository<Guid, Role> Roles { get; }

    public UnitOfWork(
        MovieReservationContext context,
        IRepository<Guid, Movie> moviesRepository,
        IRepository<Guid, MovieGenre> genreRepository,
        IRepository<Guid, Room> roomsRepository,
        IRepository<Guid, Session> sessionsRepository,
        IRepository<Guid, Ticket> ticketsRepository,
        IUserRepository usersRepository,
        IRepository<Guid, Permission> permissionsRepository,
        IRepository<Guid, UserRole> userRolesRepository,
        IRepository<Guid, Role> rolesRepository)
    {
        _context = context;
        Movies = moviesRepository;
        MovieGenres = genreRepository;
        Rooms = roomsRepository;
        Sessions = sessionsRepository;
        Tickets = ticketsRepository;
        Users = usersRepository;
        Permissions = permissionsRepository;
        UserRoles = userRolesRepository;
        Roles = rolesRepository;
    }

    public Task CompleteAsync(CancellationToken ct = default)
    {
        return _context.SaveChangesAsync(ct);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}

