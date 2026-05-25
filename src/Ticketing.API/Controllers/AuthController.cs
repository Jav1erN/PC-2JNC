using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Ticketing.Application.UserUseCases.Commands;
using Ticketing.Application.UserUseCases.DTOs;

namespace Ticketing.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public sealed class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<AuthResponse> Register(
        [FromBody] RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    [HttpPost("login")]
    public async Task<AuthResponse> Login(
        [FromBody] LoginUserCommand command,
        CancellationToken cancellationToken)
    {
        return await _mediator.Send(command, cancellationToken);
    }
}
