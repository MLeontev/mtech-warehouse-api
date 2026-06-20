using WarehouseApi.Domain.Common;

namespace WarehouseApi.Domain.Categories;

public static class CategoryErrors
{
    public static Error DuplicateName(string name) =>
        new(
            $"Категория с названием '{name}' уже существует",
            ErrorType.Conflict);
}