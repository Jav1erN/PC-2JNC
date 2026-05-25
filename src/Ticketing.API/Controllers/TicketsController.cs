using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Ticketing.API.Extensions;
using Ticketing.Application.TicketUseCases.Commands;
using Ticketing.Application.TicketUseCases.DTOs;
using Ticketing.Application.TicketUseCases.Queries;

namespace Ticketing.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class TicketsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TicketsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IReadOnlyCollection<TicketDto>> GetAll(
        CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetAllTicketsQuery(), cancellationToken);
    }

    [HttpGet("{ticketId:guid}")]
    public async Task<TicketDto> GetById(
        Guid ticketId,
        CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetTicketByIdQuery(ticketId), cancellationToken);
    }

    [HttpPost]
    public async Task<Guid> Create(
        [FromBody] CreateTicketRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateTicketCommand(User.GetUserId(), request.Title, request.Description);
        return await _mediator.Send(command, cancellationToken);
    }

    [HttpPost("{ticketId:guid}/responses")]
    [Authorize(Policy = "SupportOrAdmin")]
    public async Task<Guid> AddResponse(
        Guid ticketId,
        [FromBody] AddResponseRequest request,
        CancellationToken cancellationToken)
    {
        var command = new AddResponseToTicketCommand(
            ticketId,
            User.GetUserId(),
            request.Message);
        return await _mediator.Send(command, cancellationToken);
    }

    [HttpPatch("{ticketId:guid}/close")]
    [Authorize(Policy = "SupportOrAdmin")]
    public async Task<Unit> Close(
        Guid ticketId,
        CancellationToken cancellationToken)
    {
        return await _mediator.Send(new CloseTicketCommand(ticketId), cancellationToken);
    }
}
