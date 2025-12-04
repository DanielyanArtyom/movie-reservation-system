namespace MovieReservation.Data.Repository;

public class UserRepository: BaseRepository<User>, IUserRepository
{
    private readonly MovieReservationContext _context;
    
    public UserRepository(MovieReservationContext context) : base(context)
    {
        _context = context;
    }

    public Task<User> GetUserFullDataAsync(string email, CancellationToken ct = default)
    {
        return _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .ThenInclude(r => r.Permissions)
            .FirstOrDefaultAsync(u => u.Email == email, ct);
    }
}