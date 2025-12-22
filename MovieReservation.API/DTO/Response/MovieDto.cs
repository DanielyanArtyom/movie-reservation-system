namespace MovieReservation.API.DTO.Response;

public class MovieDto: BaseDto
{
    public required string Title { get; set; } = default!;
    public required string Description { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
        
    public required TimeSpan Duration { get; set; } = TimeSpan.Zero;

    public GenreResponse Genre { get; set; } = default!;

    //public List<Session> Sessions { get; set; } = new();
}