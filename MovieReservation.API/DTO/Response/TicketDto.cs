namespace MovieReservation.API.DTO.Response;

public class TicketDto: BaseDto
{
    public TicketStatusEnum TicketStatus { get; set; } = TicketStatusEnum.Free;
    public decimal Price { get; set; }
    public string SeatRow { get; set; } = default!;
    public int SeatNumber { get; set; }
    public SessionDto Session { get; set; } = default!;
}