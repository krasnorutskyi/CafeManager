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

    public DishController(IDishService dishService, ICategoryService categoryService, IUnitService unitService)
    {
        this._dishService = dishService;
        this._categoryService = categoryService;
        this._unitService = unitService;
        
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
    public async Task<IActionResult> Create()
    {
        var dish = new DishViewModel();
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
            Sales = dishViewModel.Sales,
            Calories = dishViewModel.Calories,
            Image = dishViewModel.Image,
            Description = dishViewModel.Description
        };

        if (!ModelState.IsValid)
        {
            dishViewModel.CategoryList = await PopulateCategoriesDropDownList();
            dishViewModel.UnitList = await PopulateUnitsDropDownList();
            return View(dishViewModel);
        }
        
        await this._dishService.AddAsync(dish);
        return RedirectToAction("Index");
    }
    
    // GET
    public async Task<IActionResult> Edit(int id)
    {
        var dish = await this._dishService.GetOneAsync(id, d => d.Category, d => d.Unit);
        var dishViewModel = new DishViewModel
        {
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
            UnitList = await PopulateUnitsDropDownList(dish.Unit.Id)
        };

        return View(dishViewModel);
    }
    
    // POST
    [HttpPost]
    public async Task<IActionResult> Edit(DishViewModel dishViewModel)
    {
        if (!ModelState.IsValid)
        {
            PopulateCategoriesDropDownList();
            PopulateUnitsDropDownList();
            return View(dishViewModel);
        }

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
            Description = dishViewModel.Description
        };
        
        await this._dishService.UpdateAsync(dish);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Details(int id)
    {
        var dish = await this._dishService.GetOneAsync(id, d => d.Category, d => d.Unit);
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