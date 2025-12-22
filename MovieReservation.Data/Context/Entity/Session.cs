namespace MovieReservation.Data.Context.Entity;

public class Session: BaseEntity
{
    public DateTime ShowTime { get; set; }
    public DateTime EndTime { get; set; }
    public SessionStatusEnum Status { get; set; } = SessionStatusEnum.Scheduled;
    
    public Guid MovieId { get; set; }
    public Movie Movie { get; set; } = default!;
    
    public Guid RoomId { get; set; }
    public Room Room { get; set; } = default!;
    
    public List<Ticket> Tickets { get; set; } = new();
}