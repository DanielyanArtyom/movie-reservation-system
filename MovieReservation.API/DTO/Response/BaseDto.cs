namespace MovieReservation.API.DTO.Response;

public class BaseDto
{
    public required Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}