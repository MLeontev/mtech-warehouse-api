using WarehouseApi.Domain.Products;

namespace WarehouseApi.Application.Products.Dtos;

public record GetProductsQuery(
    ProductStatus? Status,
    int? CategoryId,
    int Page = 1,
    int PageSize = 10);