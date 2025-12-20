namespace MovieReservation.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(
    AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, 
    Roles = AuthorizationConstants.RoleAdministrator, 
    Policy = AuthorizationConstants.WritePermissionsPolicy)]
public class MovieController: ControllerBase
{
    private readonly IMovieService _movieService;
    private readonly IMapper _mapper;
    
    public MovieController(IMovieService movieService, IMapper mapper)
    {
        _movieService = movieService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GenreResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        var roles = await _movieService.GetAllAsync();
        return Ok(_mapper.Map<List<RoleDto>>(roles));
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var role = await _movieService.GetByIdAsync(id);
        return Ok(_mapper.Map<RoleDto>(role));
    }
    
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteById(Guid id)
    {
        await _movieService.DeleteAsync(id);
        return Ok();
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] MovieCreateRequest request)
    {
        await _movieService.CreateAsync(_mapper.Map<MovieModel>(request));
        return Ok();
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRole([FromBody] MovieUpdateRequest request)
    {
        await _movieService.UpdateAsync(_mapper.Map<MovieModel>(request));
        return Ok();
    }
}