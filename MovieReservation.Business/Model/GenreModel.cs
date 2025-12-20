namespace MovieReservation.Business.Model;

public class GenreModel: BaseModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    public List<Movie> Movies { get; set; } = new();
}