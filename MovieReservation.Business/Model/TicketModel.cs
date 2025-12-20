namespace MovieReservation.Business.Model;

public class TicketModel: BaseModel
{
    public TicketStatusEnum TicketStatus { get; set; } = TicketStatusEnum.Free;
    public decimal Price { get; set; }
    public string SeatRow { get; set; } = default!;
    public int SeatNumber { get; set; }
    
    public Guid SessionId { get; set; }
    public Session Session { get; set; } = default!;

    public Guid? UserId { get; set; }
    public User? User { get; set; }
}