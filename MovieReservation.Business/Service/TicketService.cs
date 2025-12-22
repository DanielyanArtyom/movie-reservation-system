namespace MovieReservation.Business.Service;

public class TicketService: ITicketService
{
    private readonly IUnitOfWork _unitOfWork;

    public TicketService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task GenerateTicketsAsync(SessionModel session, CancellationToken ct = default)
    {
        if (session == null)
        {
            throw new BusinessException("Session not found.");
        }

        var room = await _unitOfWork.Rooms.GetByIdAsync(session.RoomId, ct);
        
        if (room == null)
        {
            throw new BusinessException("Room information is missing for session.");
        }

        var availableSeats = ResolveSeatCount(room.AvailableSeats);

        var price = ResolveTicketPrice();

        await CreateTicketsAsync(session.Id, price, availableSeats, ct);
    }

    public async Task CreateTicketsAsync(
        Guid sessionId,
        decimal price,
        int availableSeats,
        CancellationToken ct = default)
    {
        if (availableSeats <= 0)
            throw new BusinessException("Available seats count must be greater than zero.");

        // 1. Prevent duplicate ticket generation
        var existingTicketsCount = 0;

        existingTicketsCount = await _unitOfWork.Tickets.GetFilteredCountAsync(
            t => t.SessionId == sessionId,
            ct);
        
        if (existingTicketsCount > 0)
            throw new BusinessException("Tickets for this session have already been generated.");

        int seatsPerRow = 10;

        var tickets = new List<Ticket>(availableSeats);

        for (int i = 0; i < availableSeats; i++)
        {
            var rowIndex = i / seatsPerRow;
            var seatNumber = (i % seatsPerRow) + 1;

            var seatRow = ((char)('A' + rowIndex)).ToString();

            tickets.Add(new Ticket
            {
                Id = Guid.NewGuid(),
                SessionId = sessionId,
                Price = price,
                TicketStatus = TicketStatusEnum.Free,
                SeatRow = seatRow,
                SeatNumber = seatNumber,
                CreatedAt = DateTime.UtcNow
            });
        }
        _unitOfWork.Tickets.AddRange(tickets);
    }

    public Task ReserveTicketAsync(Guid id, Guid ticketId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task PurchaseTicketAsync(Guid id, Guid ticketId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task CancelTicketAsync(Guid id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
    
    
    private static int ResolveSeatCount(AvailableSeatsEnum seats) =>
        seats switch
        {
            AvailableSeatsEnum.Small => 10,
            AvailableSeatsEnum.Medium => 20,
            AvailableSeatsEnum.Large => 30,
            _ => throw new BusinessException("Unknown room capacity.")
        };
    
    private static decimal ResolveTicketPrice()
    {
        // Simple default pricing strategy
        // Can be replaced with dynamic pricing later
        return 10.00m;
    }
}