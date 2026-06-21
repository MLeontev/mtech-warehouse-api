using System.ComponentModel.DataAnnotations;

namespace WarehouseApi.Application.Products.Dtos;

public record CreateProductRequest(
    [Required(ErrorMessage = "Название товара не должно быть пустым")]
    string Name,
    
    [Required(ErrorMessage = "Артикул не должен быть пустым")]
    string Sku,
    
    [Range(1, int.MaxValue, ErrorMessage = "ID категории должен быть больше 0")]
    int CategoryId);