namespace MovieReservation.Business.Model;

public class SessionModel: BaseModel
{
    public DateTime ShowTime { get; set; }
    public SessionStatusEnum Status { get; set; } = SessionStatusEnum.Scheduled;
    
    public Guid MovieId { get; set; }
    public Movie Movie { get; set; } = default!;
    
    public Guid RoomId { get; set; }
    public Room Room { get; set; } = default!;
    
    public List<TicketModel> Tickets { get; set; } = new();
}