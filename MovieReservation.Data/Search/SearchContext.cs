
namespace MovieReservation.Data.Search;

public class SearchContext<T>
{
    public Expression<Func<T, bool>>? Filter { get; set; }
    public Expression<Func<T, object>>[]? Include { get; set; }
    public Expression<Func<T, object>>? OrderBy { get; set; }
    public bool IsAscending { get; set; } = true;

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = int.MaxValue;
   
}