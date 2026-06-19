using Microsoft.EntityFrameworkCore;
using WarehouseApi.Domain.Categories;
using WarehouseApi.Domain.Products;

namespace WarehouseApi.Infrastructure.Seed;

internal static class DatabaseSeeder
{
    public static async Task Seed(AppDbContext dbContext)
    {
        var categories = await SeedCategories(dbContext);
        await SeedProducts(dbContext, categories);
    }

    private static async Task<List<Category>> SeedCategories(AppDbContext dbContext)
    {
        if (await dbContext.Categories.AnyAsync())
            return await dbContext.Categories.ToListAsync();
        
        var categories = new List<Category>
        {
            new("Телевизоры"),
            new("Смартфоны"),
            new("Ноутбуки")
        };
        
        dbContext.Categories.AddRange(categories);
        await dbContext.SaveChangesAsync();
        
        return categories;
    }

    private static async Task SeedProducts(AppDbContext dbContext, List<Category> categories)
    {
        if (await dbContext.Products.AnyAsync())
            return;

        var products = new List<Product>
        {
            CreateProduct("TCL 55C6K 55\"", "TV-001", categories[0].Id, ProductStatus.Active),
            CreateProduct("Xiaomi TV A 43 43\"", "TV-002", categories[0].Id, ProductStatus.Defective),
            
            CreateProduct("Xiaomi REDMI 15", "PH-001", categories[1].Id, ProductStatus.Defective),
            CreateProduct("Apple iPhone 17 Pro", "PH-002", categories[1].Id, ProductStatus.WriteOff),
            
            CreateProduct("HUAWEI MateBook D 16", "LP-001", categories[2].Id, ProductStatus.Active),
            CreateProduct("HONOR MagicBook X16", "LP-002", categories[2].Id, ProductStatus.WriteOff)
        };
        
        dbContext.Products.AddRange(products);
        await dbContext.SaveChangesAsync();
    }

    private static Product CreateProduct(string name, string sku, int categoryId, ProductStatus status)
    {
        var product = new Product(name, sku, categoryId);

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