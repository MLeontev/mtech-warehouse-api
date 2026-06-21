using CSharpFunctionalExtensions;
using WarehouseApi.Domain.Categories;
using WarehouseApi.Domain.Common;

namespace WarehouseApi.Domain.Products;

public class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Sku { get; private set; } = string.Empty;
    
    public int CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;
    
    public ProductStatus Status { get; private set; }
    
    private Product() { }

    public Product(string name, string sku, int categoryId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Название товара не должно быть пустым", nameof(name));
        
        if (string.IsNullOrWhiteSpace(sku))
            throw new ArgumentException("Артикул не должен быть пустым", nameof(sku));
        
        if (categoryId <= 0)
            throw new ArgumentException("ID категории должен быть больше 0", nameof(categoryId));
        
        Name = name.Trim();
        Sku = sku.Trim().ToUpperInvariant();
        CategoryId = categoryId;
    }

    public UnitResult<Error> ChangeStatus(ProductStatus newStatus)
    {
        var isAllowed =
            (Status == ProductStatus.Active && newStatus == ProductStatus.Defective) || 
            (Status == ProductStatus.Defective && newStatus == ProductStatus.WriteOff);

        if (!isAllowed)
            return UnitResult.Failure(ProductErrors.InvalidStatusTransition(Status, newStatus));

        Status = newStatus;
        return UnitResult.Success<Error>();
    }
}