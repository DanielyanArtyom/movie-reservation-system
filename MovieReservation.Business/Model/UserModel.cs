
namespace MovieReservation.Business.Model;

public class UserModel: BaseModel
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;

    public List<UserRole> UserRoles { get; set; } = new();
    public List<Ticket> Tickets { get; set; } = new();
}