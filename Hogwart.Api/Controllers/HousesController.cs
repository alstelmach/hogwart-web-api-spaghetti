using Microsoft.AspNetCore.Mvc;

namespace Hogwart.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HousesController : ControllerBase
{
    [HttpGet]
    public Task<IActionResult> GetAsync(CancellationToken cancellationToken = default)
    {
        var houses = new List<string>
        {
            "Gryffindor",
            "Hufflepuff",
            "Ravenclaw",
            "Slytherin",
        };

        var actionResult = Ok(houses);

        return Task.FromResult<IActionResult>(actionResult);
    }
}
