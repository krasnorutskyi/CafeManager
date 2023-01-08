using CafeManager.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CafeManager.ViewModels;

public class DishViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }
    public int CategoryId { get; set; }
    public int Complexity { get; set; }
    public int Volume { get; set; }
    public int UnitId { get; set; }
    public int Sales { get; set; }
    public int Calories { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    
    public List<Product> Products { get; set; }
    public List<DishesProducts> DishesProductsList { get; set; }

    public IEnumerable<SelectListItem> CategoryList { get; set; }
    public IEnumerable<SelectListItem> UnitList { get; set; }
}