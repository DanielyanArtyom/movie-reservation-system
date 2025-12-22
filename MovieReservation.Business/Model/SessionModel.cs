namespace MovieReservation.Business.Model;

public class SessionModel : BaseModel
{
    public DateTime ShowTime { get; set; }
    public Guid MovieId { get; set; }
    public Guid RoomId { get; set; }
}