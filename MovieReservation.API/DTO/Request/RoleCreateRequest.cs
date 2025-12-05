namespace MovieReservation.API.DTO.Request;

public class RoleCreateRequest
{
    [Required] 
    public string Name { get; set; }
    
    [Required] 
    public PermissionDto Permissions { get; set; }
}