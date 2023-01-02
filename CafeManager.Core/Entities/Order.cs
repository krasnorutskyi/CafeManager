namespace CafeManager.Core.Entities;

public class Order : EntityBase
{
    public DateTime Date { get; set; }
    public float Price { get; set; }
    public float Tipp { get; set; }
    public bool HasClientsSale { get; set; }
    public float VAT { get; set; }
    public int WaiterId { get; set; }
    public Waiter Waiter { get; set; }
    public List<DishesOrders> DishesOrders { get; set; }
}