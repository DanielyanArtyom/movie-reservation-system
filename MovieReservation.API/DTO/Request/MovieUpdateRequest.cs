namespace MovieReservation.API.DTO.Request;

public class MovieUpdateRequest
{
    [Required] 
    public required Guid Id { get; set; }
    
    [Required]
    [StringLength(15, MinimumLength = 5, ErrorMessage = "Title must be between 5 and 15 characters.")]
    public required string Title { get; set; } = default!;
    
    [Required]
    [StringLength(15, MinimumLength = 5, ErrorMessage = "Description must be between 5 and 15 characters.")]
    public required string Description { get; set; } = default!;
    
    [Required]
    public required TimeSpan Duration { get; set; }
    
    [Required]
    public required Guid GenreId { get; set; }
    
    public string ImageUrl { get; set; } = default!;
}