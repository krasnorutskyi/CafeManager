using CafeManager.Application.IServices;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CafeManager.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        this._categoryService = categoryService;
    }
    
    // GET
    public async Task<IActionResult> Index(PageParameters pageParameters)
    {
        var categories = await this._categoryService.GetPageAsync(pageParameters);
        return View(categories);
    }

    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await this._categoryService.GetOneAsync(id);
        await this._categoryService.DeleteAsync(category);
        return RedirectToAction("Index");
    }
    
    // GET
    public IActionResult Create()
    {
        var category = new Category();
        return View(category);
    }
    
    // POST
    public async Task<IActionResult> CreateCategory(Category category)
    {
        await this._categoryService.AddAsync(category);
        return RedirectToAction("Index");
    }
    
    // GET
    public async Task<IActionResult> Edit(int id)
    {
        var category = await this._categoryService.GetOneAsync(id);
        return View(category);
    }
    
    // POST
    public async Task<IActionResult> EditCategory(Category category)
    {
        await this._categoryService.UpdateAsync(category);
        return RedirectToAction("Index");
    }
    
}