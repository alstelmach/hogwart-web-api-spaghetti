using Microsoft.AspNetCore.Mvc;

namespace Hogwart.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HousesController : ControllerBase
{
    [HttpGet("{houseName}")]
    public async Task<IActionResult> GetAsync(
        [FromRoute] string houseName,
        CancellationToken cancellationToken = default)
    {
        return Ok();
    }
}
