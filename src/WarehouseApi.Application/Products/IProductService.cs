using CSharpFunctionalExtensions;
using WarehouseApi.Application.Common;
using WarehouseApi.Application.Products.Dtos;
using WarehouseApi.Domain.Common;
using WarehouseApi.Domain.Products;

namespace WarehouseApi.Application.Products;

public interface IProductService
{
    Task<Result<PagedResponse<ProductResponse>, Error>> GetAll(
        GetProductsQuery query,
        CancellationToken cancellationToken = default);

    Task<Result<ProductResponse, Error>> GetById(
        int id,
        CancellationToken cancellationToken = default);
}