namespace MovieReservation.API.DTO.Response;

public class UserDto: BaseDto
{
    public required string Name { get; set; }
    public required string Email { get; set; }
}