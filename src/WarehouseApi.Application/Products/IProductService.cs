using CSharpFunctionalExtensions;
using WarehouseApi.Application.Common;
using WarehouseApi.Application.Products.Dtos;
using WarehouseApi.Domain.Common;
using WarehouseApi.Domain.Products;

namespace WarehouseApi.Application.Products;

public interface IProductService
{
    Task<PagedResponse<ProductResponse>> GetAll(
        GetProductsQuery query,
        CancellationToken cancellationToken = default);

    Task<Result<ProductResponse, Error>> GetById(
        int id,
        CancellationToken cancellationToken = default);

    Task<Result<ProductResponse, Error>> Create(
        CreateProductRequest request,
        CancellationToken cancellationToken = default);
}