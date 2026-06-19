using Microsoft.EntityFrameworkCore;
using WarehouseApi.Application.Common;
using WarehouseApi.Domain.Categories;
using WarehouseApi.Domain.Products;

namespace WarehouseApi.Infrastructure;

internal class AppDbContext : DbContext, IDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) => 
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}