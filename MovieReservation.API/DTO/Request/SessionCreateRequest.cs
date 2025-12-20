namespace MovieReservation.API.DTO.Request;

public class SessionCreateFromMovieRequest
{
    [Required]
    public DateTime ShowTime { get; set; }
    
    [Required]
    public SessionStatusEnum Status { get; set; } = SessionStatusEnum.Scheduled;
    
    [Required]
    public Guid RoomId { get; set; }
}