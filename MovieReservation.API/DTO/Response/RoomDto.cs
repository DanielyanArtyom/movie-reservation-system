namespace MovieReservation.API.DTO.Response;

public class RoomDto: BaseDto
{
    public string Name { get; set; } = default!;
    public int AvailableSeats { get; set; }
    public bool HasFreeSeats { get; set; }
}