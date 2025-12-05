namespace MovieReservation.Business.Interface;

public interface IRoleService : IBaseService<RoleModel, RoleModel>
{
    Task<List<RoleModel>> GetRolesByUser(Guid id, CancellationToken ct = default);
    Task<List<PermissionModel>> GetPermissionsByRoleId(Guid id, CancellationToken ct = default);
}