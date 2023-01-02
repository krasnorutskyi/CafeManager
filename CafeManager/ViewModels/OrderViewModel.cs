using Microsoft.AspNetCore.Mvc.Rendering;

namespace CafeManager.ViewModels;

public class OrderViewModel
{
    public DateTime Date { get; set; }
    public float Price { get; set; }
    public float Tipp { get; set; }
    public bool HasClientsSale { get; set; }
    public int WaiterId { get; set; }
    public IEnumerable<SelectListItem> WaiterList { get; set; }
}