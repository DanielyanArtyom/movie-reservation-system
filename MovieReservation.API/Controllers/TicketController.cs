using System.Security.Claims;

namespace MovieReservation.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(
    AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, 
    Roles = AuthorizationConstants.RoleUser,
    Policy = AuthorizationConstants.ReadPermissionsPolicy)]
public class TicketController: ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpPost("{ticketId}/reserve")]
    public async Task<IActionResult> Reserve(Guid ticketId, CancellationToken ct)
    {
        await _ticketService.ReserveTicketAsync(GetUserId(), ticketId, ct);
        return Ok();
    }

    [HttpPost("{ticketId}/purchase")]
    public async Task<IActionResult> Purchase(Guid ticketId, CancellationToken ct)
    {
        await _ticketService.PurchaseTicketAsync(GetUserId(), ticketId, ct);
        return Ok();
    }

    [HttpPost("{ticketId}/cancel")]
    public async Task<IActionResult> Cancel(Guid ticketId, CancellationToken ct)
    {
       await _ticketService.CancelTicketAsync(GetUserId(), ticketId, ct);
        return Ok();
    }
    
    private Guid GetUserId()
    {
        return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
    }
}