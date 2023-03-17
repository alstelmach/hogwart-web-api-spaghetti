using Hogwart.Api.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hogwart.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HousesController : ControllerBase
{
    private readonly SortingContext _context;

    public HousesController(SortingContext context)
    {
        _context = context;
    }
    
    [HttpGet("{houseName}")]
    public async Task<IActionResult> GetAsync(
        [FromRoute] string houseName,
        CancellationToken cancellationToken = default)
    {
        var house = await _context
            .Houses
            .Include(house => house.Students)
            .FirstOrDefaultAsync(
                house =>
                    house.Name == houseName,
                cancellationToken);

        return house is null
            ? NotFound()
            : Ok(house);
    }
}
