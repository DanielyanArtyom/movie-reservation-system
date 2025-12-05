

namespace MovieReservation.API.Controllers;

[Route("api/predefined")]
[ApiController]
public class PredefinedDataSetsController : ControllerBase
{
    private readonly IPredefinedDataSetsProvider _predefinedDataSetsProvider;
    
    public PredefinedDataSetsController(IPredefinedDataSetsProvider predefinedDataSetsProvider)
    {
        _predefinedDataSetsProvider = predefinedDataSetsProvider;
    }
    
    [HttpGet("pagination-options")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationOption))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetPaginationOptions()
    {
        return Ok(_predefinedDataSetsProvider.GetPaginationOptions());
    }
    
    [HttpGet("sorting-options")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetSortingOptions()
    {
        return Ok(_predefinedDataSetsProvider.GetSortingOptions());
    }
    
    [HttpGet("date-options")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetDateOptions()
    {
        return Ok(_predefinedDataSetsProvider.GetPublishDateOptions());
    }
}