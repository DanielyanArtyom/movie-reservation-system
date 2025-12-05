namespace MovieReservation.Business.Model;

public class PermissionModel: BaseModel
{
    public AccessTypesEnum AccessType { get; set; }
    public required ResourceEnum Resource { get; set; }
    
    public Guid RoleId { get; set; }
    public RoleModel Role { get; set; }
}