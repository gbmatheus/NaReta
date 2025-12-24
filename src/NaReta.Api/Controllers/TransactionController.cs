using Microsoft.AspNetCore.Mvc;
using NaReta.Application.DTO;
using NaReta.Application.UseCases.Account._Common;
using NaReta.Application.UseCases.Transaction._Common;
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
    [ProducesResponseType(typeof(OutputAccount), statusCode: StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorDTO), statusCode: StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorDTO), statusCode: StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] InputTransaction request,
        [FromQuery] int accountId,
        [FromServices] ICreateTransactionUseCase useCase)
    {
        var response = await useCase.ExecuteAsync(accountId, request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginationOutput<OutputTransaction>), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> List(
        [FromQuery] InputListTransaction queryString,
        [FromServices] IListTransactionUseCase useCase
    )
    {
        var response = await useCase.ExecuteAsync(queryString);

        if (!response.Items.Any())
            return NoContent();

        return Ok(response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(OutputAccount), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorDTO), statusCode: StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorDTO), statusCode: StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(
        [FromBody] InputTransaction request,
        [FromRoute] int id,
        [FromServices] IUpdateTrasanctionUseCase useCase)
    {
        var response = await useCase.ExecuteAsync(id, request);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorDTO), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [FromRoute] int id,
        [FromServices] IDeleteTransactionUseCase useCase)
    {
        await useCase.ExecuteAsync(id);
        return NoContent();
    }
}
