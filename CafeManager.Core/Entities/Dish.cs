namespace CafeManager.Core.Entities;

public class Dish : EntityBase
{
    public string Name { get; set; }
    public float Price { get; set; }
    public Category Category { get; set; }
    public int Complexity { get; set; }
    public int Volume { get; set; }
    public Unit Unit { get; set; } 
    public int Sales { get; set; }
    public int Calories { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    public List<DishesProducts> DishesProducts { get; set; }
    public List<DishesOrders> DishesOrders { get; set; }
}