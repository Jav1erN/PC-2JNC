using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticketing.API.Extensions;
using Ticketing.Application.DTOs.Tickets;
using Ticketing.Application.UseCases.Tickets;

namespace Ticketing.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class TicketsController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<TicketDto>>> GetAll(
        [FromServices] GetAllTicketsUseCase useCase,
        CancellationToken cancellationToken)
    {
        var tickets = await useCase.ExecuteAsync(cancellationToken);
        return Ok(tickets);
    }

    [HttpGet("{ticketId:guid}")]
    public async Task<ActionResult<TicketDto>> GetById(
        Guid ticketId,
        [FromServices] GetTicketByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var ticket = await useCase.ExecuteAsync(ticketId, cancellationToken);
        return Ok(ticket);
    }

    [HttpPost]
    public async Task<ActionResult> Create(
        [FromBody] CreateTicketRequest request,
        [FromServices] CreateTicketUseCase useCase,
        CancellationToken cancellationToken)
    {
        var ticketId = await useCase.ExecuteAsync(User.GetUserId(), request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { ticketId }, new { ticketId });
    }

    [HttpPost("{ticketId:guid}/responses")]
    public async Task<ActionResult> AddResponse(
        Guid ticketId,
        [FromBody] AddResponseRequest request,
        [FromServices] AddResponseToTicketUseCase useCase,
        CancellationToken cancellationToken)
    {
        var responseId = await useCase.ExecuteAsync(ticketId, User.GetUserId(), request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { ticketId }, new { responseId });
    }

    [HttpPatch("{ticketId:guid}/close")]
    [Authorize(Policy = "SupportOrAdmin")]
    public async Task<IActionResult> Close(
        Guid ticketId,
        [FromServices] CloseTicketUseCase useCase,
        CancellationToken cancellationToken)
    {
        await useCase.ExecuteAsync(ticketId, cancellationToken);
        return NoContent();
    }
}
