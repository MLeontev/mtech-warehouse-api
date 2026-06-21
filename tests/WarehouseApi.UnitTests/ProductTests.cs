using FluentAssertions;
using WarehouseApi.Domain.Products;

namespace WarehouseApi.UnitTests;

public class ProductTests
{
    [Theory]
    [InlineData(ProductStatus.Active, ProductStatus.Defective)]
    [InlineData(ProductStatus.Defective, ProductStatus.WriteOff)]
    public void ChangeStatus_ShouldSucceed_WhenTransitionIsAllowed(ProductStatus currentStatus, ProductStatus newStatus)
    {
        // Arrange
        var product = CreateProductWithStatus(currentStatus);

        // Act
        var result = product.ChangeStatus(newStatus);

        // Assert
        result.IsSuccess.Should().BeTrue();
        product.Status.Should().Be(newStatus);
    }
    
    [Theory]
    [InlineData(ProductStatus.Active, ProductStatus.WriteOff)]
    [InlineData(ProductStatus.Defective, ProductStatus.Active)]
    [InlineData(ProductStatus.WriteOff, ProductStatus.Defective)]
    [InlineData(ProductStatus.WriteOff, ProductStatus.Active)]
    public void ChangeStatus_ShouldFail_WhenTransitionIsNotAllowed(ProductStatus currentStatus, ProductStatus newStatus)
    {
        // Arrange
        var product = CreateProductWithStatus(currentStatus);

        // Act
        var result = product.ChangeStatus(newStatus);

        // Assert
        result.IsFailure.Should().BeTrue();
        product.Status.Should().Be(currentStatus);
    }
    
    private static Product CreateProductWithStatus(ProductStatus status)
    {
        var product = new Product("Test product", "TEST-001", 1);

        switch (status)
        {
            case ProductStatus.Defective:
                product.ChangeStatus(ProductStatus.Defective);
                break;
            case ProductStatus.WriteOff:
                product.ChangeStatus(ProductStatus.Defective);
                product.ChangeStatus(ProductStatus.WriteOff);
                break;
        }
        
        return product;
    }
}