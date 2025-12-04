namespace MovieReservation.Data.Interface;

public interface IUserRepository: IRepository<Guid, User>
{
    Task<User> GetUserFullDataAsync(string login, CancellationToken ct = default);
}