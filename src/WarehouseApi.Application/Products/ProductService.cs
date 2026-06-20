using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WarehouseApi.Application.Common;
using WarehouseApi.Application.Products.Dtos;
using WarehouseApi.Domain.Common;
using WarehouseApi.Domain.Products;

namespace WarehouseApi.Application.Products;

internal class ProductService(
    IDbContext dbContext) : IProductService
{
    public async Task<Result<PagedResponse<ProductResponse>, Error>> GetAll(
        GetProductsQuery query,
        CancellationToken cancellationToken = default)
    {
        var productsQuery = dbContext.Products.AsNoTracking();

        if (query.Status is not null)
            productsQuery = productsQuery.Where(x => x.Status == query.Status);

        if (query.CategoryId is not null)
            productsQuery = productsQuery.Where(x => x.CategoryId == query.CategoryId);

        var totalCount = await productsQuery.CountAsync(cancellationToken);

        var page = Math.Max(1, query.Page);
        var pageSize = Math.Clamp(query.PageSize, 1, 100);
        
        var items = await productsQuery
            .OrderBy(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new ProductResponse(x.Id, x.Name, x.Sku, x.CategoryId, x.Status))
            .ToListAsync(cancellationToken);
        
        return new PagedResponse<ProductResponse>(items, page, pageSize, totalCount);
    }

    public async Task<Result<ProductResponse, Error>> GetById(
        int id, 
        CancellationToken cancellationToken = default)
    {
        var product = await dbContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (product is null)
            return Result.Failure<ProductResponse, Error>(ProductErrors.NotFound(id));
        
        return Result.Success<ProductResponse, Error>(
            new ProductResponse(product.Id, product.Name, product.Sku, product.CategoryId, product.Status));
    }
}