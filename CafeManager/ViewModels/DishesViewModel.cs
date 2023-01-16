using CafeManager.Application.Paging;
using CafeManager.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CafeManager.ViewModels;

public class DishesViewModel
{
    public PagedList<Dish> Dishes { get; set; }
    public IEnumerable<SelectListItem> CategoryList { get; set; }
}