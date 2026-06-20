using WarehouseApi.Domain.Products;

namespace WarehouseApi.Application.Products.Dtos;

public record ProductResponse(
    int Id,
    string Name, 
    string Sku, 
    int CategoryId, 
    ProductStatus Status);