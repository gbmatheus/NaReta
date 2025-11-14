using Microsoft.AspNetCore.Mvc;
using NaReta.Application.UseCases.Transaction.Create;
using NaReta.Application.UseCases.Transaction.Delete;
using NaReta.Application.UseCases.Transaction.List;
using NaReta.Application.UseCases.Transaction.Update;

namespace NaReta.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] InputCreateTransaction request,
        [FromQuery] int accountId,
        [FromServices] ICreateTransactionUseCase useCase)
    {
        var response = await useCase.ExecuteAsync(accountId, request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    public async Task<IActionResult> List([FromServices] IListTransactionUseCase useCase)
    {
        var response = await useCase.ExecuteAsync();

        if (!response.Any())
            return NoContent();

        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        [FromBody] InputUpdateTransaction request,
        [FromRoute] int id,
        [FromServices] IUpdateTrasanctionUseCase useCase)
    {
        var response = await useCase.ExecuteAsync(id, request);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id,
        [FromServices] IDeleteTransactionUseCase useCase)
    {
        await useCase.ExecuteAsync(id);
        return NoContent();
    }
}
