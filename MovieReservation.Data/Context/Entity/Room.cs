namespace MovieReservation.Data.Context.Entity;

public class Room: BaseEntity
{
    public string Name { get; set; } = default!;
    public int AvailableSeats { get; set; }
    public bool HasFreeSeats { get; set; }

    public List<Session> Sessions { get; set; } = new();
}