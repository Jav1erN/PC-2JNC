using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticketing.Application.DTOs.Roles;
using Ticketing.Application.UseCases.Roles;

namespace Ticketing.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public sealed class RolesController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<RoleDto>>> GetAll(
        [FromServices] GetAllRolesUseCase useCase,
        CancellationToken cancellationToken)
    {
        var roles = await useCase.ExecuteAsync(cancellationToken);
        return Ok(roles);
    }

    [HttpPost("assign")]
    public async Task<IActionResult> AssignToUser(
        [FromBody] AssignRoleRequest request,
        [FromServices] AssignRoleToUserUseCase useCase,
        CancellationToken cancellationToken)
    {
        await useCase.ExecuteAsync(request, cancellationToken);
        return NoContent();
    }
}
