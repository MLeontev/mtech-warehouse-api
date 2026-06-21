using WarehouseApi.Domain.Common;

namespace WarehouseApi.Domain.Categories;

public static class CategoryErrors
{
    public static Error DuplicateName(string name) =>
        new(
            $"Категория с названием '{name}' уже существует",
            ErrorType.Conflict);
    
    public static Error NotFound(int id) =>
        new(
            $"Категория с ID={id} не найдена",
            ErrorType.NotFound);
}