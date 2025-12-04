namespace MovieReservation.Business.Interface;

public interface IBaseService<TRequest, TResponse>
    where TRequest : class
    where TResponse : class
{
    Task CreateAsync(TRequest request, CancellationToken ct = default);
    Task UpdateAsync(TRequest request, CancellationToken ct = default);
    Task<TResponse> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<TResponse>> GetAllAsync();
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}