using Microsoft.AspNetCore.Mvc;
using WarehouseApi.Api.Extensions;
using WarehouseApi.Application.Categories;
using WarehouseApi.Application.Categories.Dtos;

namespace WarehouseApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var categories = await categoryService.GetAll(cancellationToken);
        return Ok(categories);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var result = await categoryService.Create(request, cancellationToken);
        return result.IsSuccess
            ? Created()
            : result.Error.ToProblem(this);
    }
}