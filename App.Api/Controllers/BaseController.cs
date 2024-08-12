using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers;

[Route("api/[controller]/[action]")]
[Produces("application/json")]
[Consumes("application/json")]
[ApiController]
public class BaseController : ControllerBase
{
}
