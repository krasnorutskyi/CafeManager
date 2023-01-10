using CafeManager.Application.IServices;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;
using CafeManager.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CafeManager.Controllers;

public class DishController : Controller
{
    private readonly IDishService _dishService;
    private readonly ICategoryService _categoryService;
    private readonly IUnitService _unitService;
    private readonly IProductService _productService;
    
    public DishController(IDishService dishService, ICategoryService categoryService, IUnitService unitService, IProductService productService)
    {
        this._dishService = dishService;
        this._categoryService = categoryService;
        this._unitService = unitService;
        this._productService = productService;
    }

    // GET
    public async Task<IActionResult> Index(PageParameters pageParameters)
    {
        var dishes = await this._dishService.GetPageAsync(pageParameters);
        
        return View(dishes);
    }
    
    public async Task<IActionResult> DeleteDish(int id)
    {
        var dish = await this._dishService.GetOneAsync(id);
        await this._dishService.DeleteAsync(dish);
        return RedirectToAction("Index");
    }
    
    // GET
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var dish = new DishViewModel();
        dish.Products = (List<Product>)await this._productService.GetAllAsync();
        dish.CategoryList = await PopulateCategoriesDropDownList();
        dish.UnitList = await PopulateUnitsDropDownList();
        
        return View(dish);
    }
    
    // POST
    [HttpPost]
    public async Task<IActionResult> Create(DishViewModel dishViewModel)
    {
        var dish = new Dish
        {
            Name = dishViewModel.Name,
            Price = dishViewModel.Price,
            Category = await this._categoryService.GetOneAsync(dishViewModel.CategoryId),
            CategoryId = dishViewModel.CategoryId,
            Complexity = dishViewModel.Complexity,
            Volume = dishViewModel.Volume,
            Unit = await this._unitService.GetOneAsync(dishViewModel.UnitId),
            UnitId = dishViewModel.UnitId,
            Sales = 0,
            Calories = dishViewModel.Calories,
            Image = dishViewModel.Image,
            Description = dishViewModel.Description,
            DishesProducts = new List<DishesProducts>()
        };

        foreach (var d in dishViewModel.DishesProductsList)
        {
            d.Product = await this._productService.GetOneAsync(d.ProductId);
            d.ProductName = d.Product.Name;
            dish.DishesProducts.Add(d);
        }

        await this._dishService.AddAsync(dish);
        return RedirectToAction("Index");
    }
    
    // GET
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var dish = await this._dishService.GetOneAsync(id, d => d.Category, d => d.Unit, d => d.DishesProducts);
        var dishViewModel = new DishViewModel
        {
            //Id = id,
            Name = dish.Name,
            Price = dish.Price,
            CategoryId = dish.CategoryId,
            Complexity = dish.Complexity,
            Volume = dish.Volume,
            UnitId = dish.UnitId,
            Sales = dish.Sales,
            Calories = dish.Calories,
            Image = dish.Image,
            Description = dish.Description,
            CategoryList = await PopulateCategoriesDropDownList(dish.Category.Id),
            UnitList = await PopulateUnitsDropDownList(dish.Unit.Id),
            DishesProductsList = dish.DishesProducts,
            Products = (List<Product>)await this._productService.GetAllAsync()
        };

        return View(dishViewModel);
    }
    
    // POST
    [HttpPost]
    public async Task<IActionResult> Edit(DishViewModel dishViewModel)
    {

        var dish = new Dish()
        {
            Id = dishViewModel.Id,
            Name = dishViewModel.Name,
            Price = dishViewModel.Price,
            Category = await this._categoryService.GetOneAsync(dishViewModel.CategoryId),
            CategoryId = dishViewModel.CategoryId,
            Complexity = dishViewModel.Complexity,
            Volume = dishViewModel.Volume,
            Unit = await this._unitService.GetOneAsync(dishViewModel.UnitId),
            UnitId = dishViewModel.UnitId,
            Sales = dishViewModel.Sales,
            Calories = dishViewModel.Calories,
            Image = dishViewModel.Image,
            Description = dishViewModel.Description,
            DishesProducts = new List<DishesProducts>()
        };

        foreach (var d in dishViewModel.DishesProductsList)
        {
            d.Product = await this._productService.GetOneAsync(d.ProductId);
            d.ProductName = d.Product.Name;
            dish.DishesProducts.Add(d);
        }
        
        await this._dishService.UpdateAsync(dish);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Details(int id)
    {
        var dish = await this._dishService.GetOneAsync(id, d => d.Category, d => d.Unit, d=>d.DishesProducts);
        return View(dish);
    }
    
    private async Task<IEnumerable<SelectListItem>> PopulateCategoriesDropDownList(object selectedCategory = null)
    {
        var categories = await this._categoryService.GetAllAsync();
        return new SelectList(categories, "Id", "Name", selectedCategory);
    } 
    
    private async Task<IEnumerable<SelectListItem>> PopulateUnitsDropDownList(object selectedUnit = null)
    {
        var units = await this._unitService.GetAllAsync();
        return new SelectList(units, "Id", "Name", selectedUnit);
    } 
}