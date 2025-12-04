
namespace MovieReservation.API.DTO.Request;

public class LoginRequest
{
    [Required]
    [StringLength(15, MinimumLength = 5, ErrorMessage = "Email must be between 5 and 15 characters.")]
    public required string Email { get; set; }
    
    [Required]
    [StringLength(15, MinimumLength = 5, ErrorMessage = "Password must be between 5 and 15 characters.")]
    public required string Password { get; set; }
}