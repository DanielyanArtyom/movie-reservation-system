using System.Security.Claims;
using MovieReservation.Business.Exceptions;

namespace MovieReservation.API.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetCustomerId(this ClaimsPrincipal user)
    {
        var customerIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (customerIdClaim == null || !Guid.TryParse(customerIdClaim, out Guid customerId))
        {
            throw new UnauthorizedException("Invalid or missing customerId in token.");
        }

        return customerId;
    }

    public static string GetName(this ClaimsPrincipal user)
    {
        var userName = user.FindFirst(ClaimTypes.Name)?.Value;

        if (string.IsNullOrEmpty(userName))
        {
            throw new UnauthorizedAccessException("Invalid or missing user name in token.");
        }

        return userName;
    }
}