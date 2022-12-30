namespace CafeManager.Core.Entities;

public class Category : EntityBase
{
    public string Name { get; set; }
    public List<Dish?> Dishes { get; set; }
}