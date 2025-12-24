namespace MovieReservation.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(
    AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, 
    Roles = AuthorizationConstants.RoleAdministrator,
    Policy = AuthorizationConstants.WritePermissionsPolicy)]
public class SessionController: ControllerBase
{
    private readonly ISessionService _sessionService;
    private readonly IMapper _mapper;

    public SessionController(ISessionService sessionService, IMapper mapper)
    {
        _sessionService = sessionService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SessionDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        var roles = await _sessionService.GetAllAsync();
        return Ok(_mapper.Map<List<SessionDto>>(roles));
    }
    
    [HttpGet("{id}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SessionDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var role = await _sessionService.GetByIdAsync(id);
        return Ok(_mapper.Map<SessionDto>(role));
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteById(Guid id)
    {
        await _sessionService.DeleteAsync(id);
        return Ok();
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] SessionCreateRequest request)
    {
        await _sessionService.CreateAsync(_mapper.Map<SessionModel>(request));
        return Ok();
    }
}