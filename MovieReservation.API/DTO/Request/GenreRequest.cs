namespace MovieReservation.API.DTO.Request;

public class GenreRequest
{
    [Required]
    [StringLength(15, MinimumLength = 5, ErrorMessage = "Name must be between 5 and 15 characters.")]
    public required string Name { get; set; }
    
    [Required]
    [StringLength(15, MinimumLength = 5, ErrorMessage = "Description must be between 5 and 15 characters.")]
    public required string Description { get; set; }
}