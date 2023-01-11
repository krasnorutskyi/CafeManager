using CafeManager.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CafeManager.ViewModels;

public class OrderViewModel
{
    public float Tipp { get; set; }
    public bool HasClientsSale { get; set; }
    public int WaiterId { get; set; }
    
    public int TableId { get; set; }
    public List<Dish> Dishes { get; set; }

    public List<DishesOrders> DishesOrdersList{ get; set; }
    public IEnumerable<SelectListItem> WaiterList { get; set; }
    public IEnumerable<SelectListItem> TableList { get; set; }
}