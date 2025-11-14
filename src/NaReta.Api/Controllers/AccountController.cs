using Microsoft.AspNetCore.Mvc;
using NaReta.Application.UseCases.Account.Create;
using NaReta.Application.UseCases.Account.Get;

namespace NaReta.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
       [FromBody] InputCreateAccount request,
       [FromServices] ICreateAccountUseCase useCase)
    {
        var response = await useCase.ExecuteAsync(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    public async Task<IActionResult> List([FromServices] IListAccountUseCase useCase)
    {
        var response = await useCase.ExecuteAsync();

        if (!response.Any())
            return NoContent();

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(
        [FromRoute] int id,
        [FromServices] IGetAccountUseCase useCase)
    {
        var response = await useCase.ExecuteAsync(id);
        return Ok(response);
    }
}
