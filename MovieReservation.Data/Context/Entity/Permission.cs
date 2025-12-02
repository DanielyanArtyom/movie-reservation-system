namespace MovieReservation.Data.Context.Entity;

public class Permission: BaseEntity
{
    public AccessTypesEnum AccessType { get; set; }
    public ResourceEnum Resource { get; set; }

    public Guid RoleId { get; set; }
    public Role Role { get; set; } = default!;
}