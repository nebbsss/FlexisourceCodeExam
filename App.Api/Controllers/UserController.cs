using App.Services.User.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers;

public class UserController : BaseController
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        ArgumentNullException.ThrowIfNull(nameof(mediator));
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
}
