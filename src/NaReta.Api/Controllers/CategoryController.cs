using Microsoft.AspNetCore.Mvc;
using NaReta.Application.DTO;
using NaReta.Application.UseCases.Category._Common;
using NaReta.Application.UseCases.Category.Create;
using NaReta.Application.UseCases.Category.List;
using NaReta.Application.UseCases.Category.Update;

namespace NaReta.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(OutputCategory), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorDTO), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] InputCategory request, [FromServices] ICreateCategoryUseCase useCase)
    {
        var response = await useCase.ExecuteAsync(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<OutputCategory>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> List([FromServices] IListCategoryUseCase useCase)
    {
        var response = await useCase.ExecuteAsync();

        if (!response.Any())
            return NoContent();

        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(typeof(OutputCategory), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorDTO), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorDTO), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] InputCategory request,
        [FromServices] IUpdateCategoryUseCase useCase)
    {
        var response = await useCase.ExecuteAsync(id, request);

        return Ok(response);
    }
}
