namespace CafeManager.Core.Entities;

public class DishesOrders
{
    public int DishId { get; set; }
    public Dish Dish { get; set; }
    public int OrdersNumber { get; set; }
    public Order Order { get; set; }
    
    public int DishesAmount { get; set; }
    
    public float DishesTotal { get; set; }
}