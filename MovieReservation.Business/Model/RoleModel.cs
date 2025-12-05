namespace MovieReservation.Business.Model;

public class RoleModel: BaseModel
{
    public string Name { get; set; }
    
    public List<UserRole> UserRoles { get; set; } = new();
    public List<PermissionModel> Permissions { get; set; } = new();
}