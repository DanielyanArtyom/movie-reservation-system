namespace MovieReservation.API.DTO.Response;

public class GenreResponse: BaseDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }

    public List<string> Movies { get; set; } = new List<string>();
}