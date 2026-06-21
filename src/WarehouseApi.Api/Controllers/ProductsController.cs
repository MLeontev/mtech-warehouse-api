using Microsoft.AspNetCore.Mvc;
using WarehouseApi.Api.Extensions;
using WarehouseApi.Application.Products;
using WarehouseApi.Application.Products.Dtos;

namespace WarehouseApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] GetProductsQuery query,
        CancellationToken cancellationToken)
    {
        var products = await productService.GetAll(query, cancellationToken);
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        var result = await productService.GetById(id, cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.Error.ToProblem(this);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        var result = await productService.Create(request, cancellationToken);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value)
            : result.Error.ToProblem(this);
    }
}