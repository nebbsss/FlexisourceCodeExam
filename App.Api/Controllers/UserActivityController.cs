using App.Services.UserActivity.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers;

public class UserActivityController : BaseController
{
    private readonly IMediator _mediator;

    public UserActivityController(IMediator mediator)
    {
        ArgumentNullException.ThrowIfNull(nameof(mediator));
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserActivityCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
}
