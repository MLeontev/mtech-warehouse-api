using System.ComponentModel.DataAnnotations;
using WarehouseApi.Domain.Products;

namespace WarehouseApi.Application.Products.Dtos;

public record UpdateProductStatusRequest(
    [Required(ErrorMessage = "Статус товара не должен быть пустым")]
    [EnumDataType(typeof(ProductStatus), ErrorMessage = "Указан несуществующий статус товара")]
    ProductStatus? Status);