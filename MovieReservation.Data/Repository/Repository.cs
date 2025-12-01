namespace MovieReservation.Data.Repository;

public class BaseRepository<T> : IRepository<Guid, T> where T : BaseEntity
{
    private readonly MovieReservationContext _context;
    private readonly DbSet<T> _dbSet;

    public BaseRepository(MovieReservationContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
    
    public void Add(T entity)
    {
        _dbSet.Add(entity);
    }
    
    public void AddRange(List<T> entities)
    {
        _dbSet.AddRange(entities);
    }

    public void Update(Guid id, T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void DeleteById(Guid id)
    {
        _dbSet.Where(x => x.Id == id).ExecuteDelete();
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<int> GetTotalCountAsync(CancellationToken ct = default)
    {
        return await _dbSet.CountAsync(ct);
    }
    
    public async Task<int> GetFilteredCountAsync(Expression<Func<T, bool>>? filter, CancellationToken ct = default)
    {
        IQueryable<T> query = _context.Set<T>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.CountAsync(ct);
    }

    public async Task<SearchResult<T>> SearchAsync(SearchContext<T> context, CancellationToken ct = default)
    {
        IQueryable<T> query = _context.Set<T>();

        if (context.Filter != null)
        {
            query = query.Where(context.Filter);
        }

        if (context.Include != null)
        {
            foreach (var includeExpression in context.Include)
            {
                query = query.Include(includeExpression);
            }
        }

        int totalCount = await query.CountAsync(ct);

        if (context.OrderBy != null)
        {
            query = context.IsAscending
                ? query.OrderBy(context.OrderBy)
                : query.OrderByDescending(context.OrderBy);
        }

        if (context.PageSize < int.MaxValue)
        {
            var skip = (context.PageNumber - 1) * context.PageSize;
            query = query.Skip(skip).Take(context.PageSize);
        }

        var results = await query.ToListAsync(ct);

        return new SearchResult<T>
        {
            TotalCount = totalCount,
            Results = results
        };
    }
}