namespace MovieReservation.Business.Interface;

public interface ITicketService
{
    Task GenerateTicketsAsync(SessionModel session, CancellationToken ct = default);
    Task CreateTicketsAsync(Guid sessionId, decimal price, int avab, CancellationToken ct = default);
    Task ReserveTicketAsync(Guid userId, Guid ticketId, CancellationToken ct = default);
    Task PurchaseTicketAsync(Guid userId, Guid ticketId, CancellationToken ct = default);
    Task CancelTicketAsync(Guid userId, Guid ticketId, CancellationToken ct = default);
}