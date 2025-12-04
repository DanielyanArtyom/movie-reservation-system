

namespace MovieReservation.API.DTO.Request;

public class CheckAccessRequest
{
    [Required]
    public required string TargetPage { get; set; }
    
    [Required]
    public required AccessTypesEnum Permission { get; set; }
}