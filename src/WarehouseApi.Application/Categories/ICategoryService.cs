using CSharpFunctionalExtensions;
using WarehouseApi.Application.Categories.Dtos;
using WarehouseApi.Domain.Common;

namespace WarehouseApi.Application.Categories;

public interface ICategoryService
{
    Task<List<CategoryResponse>> GetAll(CancellationToken cancellationToken = default);
    
    Task<Result<CategoryResponse, Error>> Create(
        CreateCategoryRequest request, 
        CancellationToken cancellationToken = default);
}