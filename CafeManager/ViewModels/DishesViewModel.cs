using CafeManager.Application.Paging;
using CafeManager.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CafeManager.ViewModels;

public class DishesViewModel
{
    public string Search { get; set; }
    public int CategoryId { get; set; }
    public PagedList<Dish> Dishes { get; set; }
    public IEnumerable<SelectListItem> CategoryList { get; set; }
}