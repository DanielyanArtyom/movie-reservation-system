namespace MovieReservation.Data.Context.Entity;

public class MovieGenre: BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    public List<Movie> Movies { get; set; } = new();
}