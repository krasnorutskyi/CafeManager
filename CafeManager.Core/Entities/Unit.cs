namespace CafeManager.Core.Entities;

public class Unit : EntityBase
{
    public string Name { get; set; }
    public List<Dish?> Dishes { get; set; }
}