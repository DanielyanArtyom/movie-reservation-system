using System.ComponentModel.DataAnnotations;

namespace MovieReservation.API.DTO.Request;

public class RegisterRequest
{
    [Required]
    [StringLength(15, MinimumLength = 5, ErrorMessage = "Name must be between 5 and 15 characters.")]
    public required string Name { get; set; }
    
    [Required]
    [StringLength(15, MinimumLength = 5, ErrorMessage = "Password must be between 5 and 15 characters.")]
    public required string Password { get; set; }
    
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    public required string Email { get; set; }
}