

namespace MovieReservation.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(
    AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, 
    Roles = AuthorizationConstants.RoleAdministrator,
    Policy = AuthorizationConstants.WritePermissionsPolicy)]
public class RolesController: ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IRoleService _roleservice;
    private IPredefinedDataSetsProvider _predefinedDataSetsProvider;

    public RolesController(
        IRoleService roleService,
        IPredefinedDataSetsProvider predefinedDataSetsProvider,
        IMapper mapper)
    {
        _mapper = mapper;
        _roleservice = roleService;
        _predefinedDataSetsProvider = predefinedDataSetsProvider;
    }
    
     [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RoleDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetALlRoles()
    {
        var roles = await _roleservice.GetAllAsync();
        return Ok(_mapper.Map<List<RoleDto>>(roles));
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, 
        Roles = AuthorizationConstants.RoleAdministrator, 
        Policy = AuthorizationConstants.ReadPermissionsPolicy)]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var role = await _roleservice.GetByIdAsync(id);
        return Ok(_mapper.Map<RoleDto>(role));
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRoleById(Guid id)
    {
        await _roleservice.DeleteAsync(id);
        return Ok();
    }
    
    [HttpGet("permissions")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<string>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetAllPermissions()
    {
        return Ok(_predefinedDataSetsProvider.GetAllPermissions());
    }
    
    [HttpGet("{id}/permissions")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PermissionDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPermissionsByRoleId(Guid id)
    {
        var permissions = await _roleservice.GetPermissionsByRoleId(id);

        return Ok(_mapper.Map<List<PermissionDto>>(permissions));
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateRole([FromBody] RoleCreateRequest request)
    {
        await _roleservice.CreateAsync(_mapper.Map<RoleModel>(request));

        return Ok();
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRole([FromBody] RoleUpdateRequest request)
    {
        await _roleservice.UpdateAsync(_mapper.Map<RoleModel>(request));

        return Ok();
    }
}