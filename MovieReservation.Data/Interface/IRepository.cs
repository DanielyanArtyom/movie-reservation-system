

namespace MovieReservation.Data.Interface;

public interface IRepository<TKey, T>  where T : BaseEntity
{
    void Add(T entity);
    
    void AddRange(List<T> entities);

    void Update(TKey id, T entity);

    void Delete(T entity);

    void DeleteById(TKey id);

    Task<T?> GetByIdAsync(TKey id, CancellationToken ct = default);

    Task<int> GetTotalCountAsync(CancellationToken ct = default);
    
    Task<int> GetFilteredCountAsync(Expression<Func<T, bool>>? filter, CancellationToken ct = default);

    Task<SearchResult<T>> SearchAsync(SearchContext<T> context, CancellationToken ct = default);
}
