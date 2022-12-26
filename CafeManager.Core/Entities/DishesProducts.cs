namespace CafeManager.Core.Entities;

public class DishesProducts
{
    public int DishId { get; set; }
    public Dish Dish { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    
    public float ProductsAmount { get; set; }
}