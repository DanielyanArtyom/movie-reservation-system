namespace MovieReservation.API.DTO.Request;

public class SessionCreateRequest
{
    [Required]
    public required DateTime ShowTime { get; set; }
    
    [Required]
    public required SessionStatusEnum Status { get; set; } = SessionStatusEnum.Scheduled;
    
    [Required]
    public required Guid RoomId { get; set; }
    
    [Required]
    public required Guid MovieId { get; set; }
}