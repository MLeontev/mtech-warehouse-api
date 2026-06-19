using Microsoft.EntityFrameworkCore;
using WarehouseApi.Domain.Categories;
using WarehouseApi.Domain.Products;

namespace WarehouseApi.Application.Common;

public interface IDbContext
{
    DbSet<Product> Products { get; }
    DbSet<Category> Categories { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}