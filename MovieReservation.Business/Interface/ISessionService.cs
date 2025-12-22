namespace MovieReservation.Business.Interface;

public interface ISessionService: IBaseService<SessionModel, SessionModel>
{
    Task<List<SessionModel>> GetAllByDateAsync(DateTime date, CancellationToken ct = default);
    Task<SessionModel> GetByMovieIdAsync(Guid movieId, CancellationToken ct = default);
}