namespace MovieReservation.Business.Model;

public class MovieModel: BaseModel
{
    public required string Title { get; set; } = default!;
    public required string Description { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
        
    public required TimeSpan Duration { get; set; } = TimeSpan.Zero;

    public required Guid GenreId { get; set; }
    public MovieGenre Genre { get; set; } = default!;

    public List<Session> Sessions { get; set; } = new();
}