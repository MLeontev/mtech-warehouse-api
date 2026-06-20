using WarehouseApi.Domain.Common;

namespace WarehouseApi.Domain.Products;

public static class ProductErrors
{
    public static Error InvalidStatusTransition(ProductStatus from, ProductStatus to) =>
        new(
            $"Переход из статуса '{from}' в '{to}' невозможен",
            ErrorType.Conflict);
    
    public static Error NotFound(int id) =>
        new(
            $"Товар с ID={id} не найден",
            ErrorType.NotFound);
}