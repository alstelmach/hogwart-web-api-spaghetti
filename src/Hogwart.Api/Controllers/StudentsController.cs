using Hogwart.Api.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Hogwart.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync(
        [FromBody] StudentDto studentDto,
        CancellationToken cancellationToken = default)
    {
        return Ok();
    }
}
