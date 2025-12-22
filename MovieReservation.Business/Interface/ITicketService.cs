namespace MovieReservation.Business.Interface;

public interface ITicketService
{
    Task GenerateTicketsAsync(SessionModel session, CancellationToken ct = default);
    Task CreateTicketsAsync(Guid sessionId, decimal price, int avab, CancellationToken ct = default);
    Task ReserveTicketAsync(Guid id, Guid ticketId, CancellationToken ct = default);
    Task PurchaseTicketAsync(Guid id, Guid ticketId, CancellationToken ct = default);
    Task CancelTicketAsync(Guid id, CancellationToken ct = default);
}