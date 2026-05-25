using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Ticketing.Application.RoleUseCases.Commands;
using Ticketing.Application.RoleUseCases.DTOs;
using Ticketing.Application.RoleUseCases.Queries;

namespace Ticketing.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public sealed class RolesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RolesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IReadOnlyCollection<RoleDto>> GetAll(
        CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetAllRolesQuery(), cancellationToken);
    }

    [HttpPost("assign")]
    public async Task<Unit> AssignToUser(
        [FromBody] AssignRoleToUserCommand command,
        CancellationToken cancellationToken)
    {
        return await _mediator.Send(command, cancellationToken);
    }
}
