using System.ComponentModel.DataAnnotations;

namespace WarehouseApi.Application.Categories.Dtos;

public record CreateCategoryRequest(
    [Required(ErrorMessage = "Название категории не должно быть пустым")] 
    string Name);