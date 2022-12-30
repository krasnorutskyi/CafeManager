using Microsoft.AspNetCore.Mvc.Rendering;

namespace CafeManager.ViewModels;

public class TableViewModel
{
    public int Id { get; set; }
    public int PlacesNumber { get; set; }
    public int WaiterId { get; set; }
    public IEnumerable<SelectListItem> WaiterList { get; set; }
}