namespace MovieReservation.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(
    AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, 
    Roles = AuthorizationConstants.RoleAdministrator, 
    Policy = AuthorizationConstants.WritePermissionsPolicy)]
public class GenreController: ControllerBase
{
    private readonly IGenreService _genreService;
    private readonly IMapper _mapper;
    
    public GenreController(IGenreService genreService, IMapper mapper)
    {
        _genreService = genreService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GenreResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetALlGenres()
    {
        var roles = await _genreService.GetAllAsync();
        return Ok(_mapper.Map<List<RoleDto>>(roles));
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetGenreById(Guid id)
    {
        var role = await _genreService.GetByIdAsync(id);
        return Ok(_mapper.Map<RoleDto>(role));
    }
    
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteGenreById(Guid id)
    {
        await _genreService.DeleteAsync(id);
        return Ok();
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateRole([FromBody] GenreRequest request)
    {
        await _genreService.CreateAsync(_mapper.Map<GenreModel>(request));
        return Ok();
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRole([FromBody] GenreUpdateModel request)
    {
        await _genreService.UpdateAsync(_mapper.Map<GenreModel>(request));
        return Ok();
    }
}