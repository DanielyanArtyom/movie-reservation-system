namespace MovieReservation.Data.Search;

public class SearchResult<T>
{
    public List<T> Results { get; set; }

    public int TotalCount { get; set; }
}