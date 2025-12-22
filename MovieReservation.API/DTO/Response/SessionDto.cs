namespace MovieReservation.API.DTO.Response;

public class SessionDto: BaseDto
{
    public DateTime ShowTime { get; set; }
    public SessionStatusEnum Status { get; set; } = SessionStatusEnum.Scheduled;
    
    public Guid MovieId { get; set; }
    public MovieDto Movie { get; set; } = default!;
    
    public Guid RoomId { get; set; }
    public RoomDto Room { get; set; } = default!;
    
    public List<TicketDto> Tickets { get; set; } = new();
}