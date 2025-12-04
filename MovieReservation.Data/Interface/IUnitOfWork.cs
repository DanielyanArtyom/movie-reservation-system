namespace MovieReservation.Data.Interface;

public interface IUnitOfWork
{
    IRepository<Guid, Movie> Movies { get; }
    IRepository<Guid, MovieGenre> MovieGenres { get; }
    IRepository<Guid, Room> Rooms { get; }
    IRepository<Guid, Session> Sessions { get; }
    IRepository<Guid, Ticket> Tickets { get; }
    public IUserRepository Users { get; }
    IRepository<Guid, Permission> Permissions { get; }
    IRepository<Guid, UserRole> UserRoles { get; }

    IRepository<Guid, Role> Roles { get; }
    
    Task CompleteAsync(CancellationToken ct = default );
    void Dispose();
}
