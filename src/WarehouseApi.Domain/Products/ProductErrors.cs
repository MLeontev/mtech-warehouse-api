using WarehouseApi.Domain.Common;

namespace WarehouseApi.Domain.Products;

public static class ProductErrors
{
    public static Error InvalidStatusTransition(ProductStatus from, ProductStatus to) =>
        new(
            "Product.InvalidStatusTransition",
            $"Переход из статуса '{from}' в '{to}' невозможен",
            ErrorType.Conflict);
    
    public static Error NotFound(int id) =>
        new(
            "Product.NotFound",
            $"Товар с ID={id} не найден",
            ErrorType.NotFound);
}