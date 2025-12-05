namespace MovieReservation.API.DTO.Request;

public class RoleUpdateRequest
{
    [Required] 
    public required Guid Id { get; set; }
    
    [Required] 
    public string Name { get; set; }
    
    [Required] 
    public PermissionDto Permissions { get; set; }
}