using Microsoft.AspNetCore.Mvc;
using NaReta.Application.DTO;
using NaReta.Application.UseCases.Account._Common;
using NaReta.Application.UseCases.Account.Create;
using NaReta.Application.UseCases.Account.Get;

namespace NaReta.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(OutputAccount), statusCode: StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorDTO), statusCode: StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
       [FromBody] InputCreateAccount request,
       [FromServices] ICreateAccountUseCase useCase)
    {
        var response = await useCase.ExecuteAsync(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<OutputAccount>), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    public async Task<IActionResult> List([FromServices] IListAccountUseCase useCase)
    {
        var response = await useCase.ExecuteAsync();

        if (!response.Any())
            return NoContent();

        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(OutputAccount), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorDTO), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(
        [FromRoute] int id,
        [FromServices] IGetAccountUseCase useCase)
    {
        var response = await useCase.ExecuteAsync(id);
        return Ok(response);
    }
}
