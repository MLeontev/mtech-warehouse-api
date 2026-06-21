using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using WarehouseApi.Application.Common;
using WarehouseApi.Application.Products.Dtos;
using WarehouseApi.Domain.Categories;
using WarehouseApi.Domain.Common;
using WarehouseApi.Domain.Products;

namespace WarehouseApi.Application.Products;

internal class ProductService(
    IDbContext dbContext) : IProductService
{
    public async Task<PagedResponse<ProductResponse>> GetAll(
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

    public async Task<Result<ProductResponse, Error>> Create(
        CreateProductRequest request, 
        CancellationToken cancellationToken = default)
    {
        var categoryExists = await dbContext.Categories
            .AnyAsync(x => x.Id == request.CategoryId, cancellationToken);

        if (!categoryExists)
            return Result.Failure<ProductResponse, Error>(CategoryErrors.NotFound(request.CategoryId));

        var normalizedSku = request.Sku.Trim().ToUpperInvariant();

        var skuTaken = await dbContext.Products
            .AnyAsync(x => x.Sku == normalizedSku, cancellationToken);
        
        if (skuTaken)
            return Result.Failure<ProductResponse, Error>(ProductErrors.DuplicateSku(request.Sku));

        var product = new Product(request.Name, request.Sku, request.CategoryId);

        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        var response = new ProductResponse(product.Id, product.Name, product.Sku, product.CategoryId, product.Status);
        return Result.Success<ProductResponse, Error>(response);
    }
}