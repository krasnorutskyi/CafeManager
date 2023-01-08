namespace CafeManager.Core.Entities;

public class DishesProducts
{
    public int DishId { get; set; }
    public Dish Dish { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public Product Product { get; set; }
    
    public int ProductsAmount { get; set; }
}