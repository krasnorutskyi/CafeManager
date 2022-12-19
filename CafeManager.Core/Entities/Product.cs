namespace CafeManager.Core.Entities;

public class Product : EntityBase
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public float Price { get; set; }
    public DateTime BestBefore { get; set; }
    public List<DishesProducts> DishesProducts { get; set; }
}