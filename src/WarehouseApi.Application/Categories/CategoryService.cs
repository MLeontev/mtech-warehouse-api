using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using WarehouseApi.Application.Categories.Dtos;
using WarehouseApi.Application.Common;
using WarehouseApi.Domain.Categories;
using WarehouseApi.Domain.Common;

namespace WarehouseApi.Application.Categories;

internal class CategoryService(IDbContext dbContext) : ICategoryService
{
    public async Task<List<CategoryResponse>> GetAll(CancellationToken cancellationToken = default)
    {
        return await dbContext.Categories
            .AsNoTracking()
            .Select(x => new CategoryResponse(x.Id, x.Name))
            .ToListAsync(cancellationToken);
    }

    public async Task<Result<CategoryResponse, Error>> Create(
        CreateCategoryRequest request, 
        CancellationToken cancellationToken = default)
    {
        var normalizedName = request.Name.Trim();
        
        var exists = await dbContext.Categories
            .AnyAsync(x => x.Name.ToLower() == normalizedName.ToLower(), cancellationToken: cancellationToken);
        
        if (exists)
            return Result.Failure<CategoryResponse, Error>(CategoryErrors.DuplicateName(normalizedName));

        var category = new Category(normalizedName);

        dbContext.Categories.Add(category);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return Result.Success<CategoryResponse, Error>(new CategoryResponse(category.Id, category.Name));
    }
}