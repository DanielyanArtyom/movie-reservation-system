namespace MovieReservation.Data.Context.Entity;

public class Movie: BaseEntity
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
        
    public TimeSpan Duration { get; set; }

    public Guid GenreId { get; set; }
    public MovieGenre Genre { get; set; } = default!;

    public List<Session> Sessions { get; set; } = new();
}