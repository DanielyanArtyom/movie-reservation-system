namespace MovieReservation.Data.Context.Entity;

public class Role: BaseEntity
{
    public string Name { get; set; } = default!;

    public List<UserRole> UserRoles { get; set; } = new();
    public List<Permission> Permissions { get; set; } = new();
}