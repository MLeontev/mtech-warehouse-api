using WarehouseApi.Domain.Common;

namespace WarehouseApi.Domain.Products;

public static class ProductErrors
{
    public static Error InvalidStatusTransition(ProductStatus from, ProductStatus to) =>
        new("Product.InvalidStatusTransition",
            $"Transition from '{from}' to '{to}' is not allowed",
            ErrorType.Conflict);
}