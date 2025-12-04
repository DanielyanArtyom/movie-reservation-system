using AutoMapper;
using MovieReservation.API.DTO.Response;
using MovieReservation.Business.Interface;

namespace MovieReservation.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(
    AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, 
    Roles = AuthorizationConstants.RoleAdministrator, 
    Policy = AuthorizationConstants.WritePermissionsPolicy)]
public class UsersController: ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;

    public UsersController(IUserService userService,IRoleService roleService,IMapper mapper)
    {
        _mapper = mapper;
        _userService = userService;
        _roleService = roleService;
    }
    
    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        await _userService.CreateAsync(_mapper.Map<UserModel>(request));
        return Ok();
    }
    
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthorizationDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var result = await _userService.Login(_mapper.Map<UserModel>(request));
        
        return Ok(_mapper.Map<AuthorizationDto>(result));
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser([FromBody] UserUpdateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        await _userService.UpdateAsync(_mapper.Map<UserModel>(request));
        
        return Ok();
    }
    
    [HttpPost("access")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CheckAccesses([FromBody] CheckAccessRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        await _userService.CheckAccesses(_mapper.Map<CheckAccessModel>(request));
        return Ok();
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetALlUsers()
    {
        var users = await _userService.GetAllAsync();
        return Ok(_mapper.Map<List<UserDto>>(users));
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var users = await _userService.GetByIdAsync(id);
        return Ok(_mapper.Map<UserDto>(users));
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUserById(Guid id)
    {
        await _userService.DeleteAsync(id);
        return Ok();
    }
    
    [HttpDelete("{id}/roles")]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(RoleDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRolesByUserId(Guid id)
    {
       // await _roleService.GetRolesByUser(id);
       await Task.Delay(10);
        return Ok();
    }
}