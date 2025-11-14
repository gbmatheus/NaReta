using Microsoft.AspNetCore.Mvc;
using NaReta.Application.UseCases.Category.Create;
using NaReta.Application.UseCases.Category.List;
using NaReta.Application.UseCases.Category.Update;

namespace NaReta.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] InputCreateCategory request, [FromServices] ICreateCategoryUseCase useCase)
    {
        var response = await useCase.ExecuteAsync(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    public async Task<IActionResult> List([FromServices] IListCategoryUseCase useCase)
    {
        var response = await useCase.ExecuteAsync();

        if (!response.Any())
            return NoContent();

        return Ok(response);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] InputUpdateCategory request,
        [FromServices] IUpdateCategoryUseCase useCase)
    {
        var response = await useCase.ExecuteAsync(id, request);

        return Created(string.Empty, response);
    }
}
