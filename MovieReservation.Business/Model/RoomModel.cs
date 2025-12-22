namespace MovieReservation.Business.Model;

public class RoomModel : BaseModel
{
    public string Name { get; set; } = default!;
    public AvailableSeatsEnum AvailableSeats { get; set; }
}