namespace WarehouseApi.Domain.Categories;

public class Category
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    
    private Category() { }

    public Category(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Название категории не должно быть пустым", nameof(name));
        
        Name = name.Trim();
    }
}