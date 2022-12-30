using CafeManager.Application.IServices;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CafeManager.Controllers;

public class TableController : Controller
{
    private readonly ITableService _tableService;

    public TableController(ITableService tableService)
    {
        this._tableService = tableService;
    }
    
    // GET
    public async Task<IActionResult> Index(PageParameters pageParameters)
    {
        var tables = await this._tableService.GetPageAsync(pageParameters); 
        return View(tables);
    }
    
    public async Task<IActionResult> DeleteTable(int id)
    {
        var table = await this._tableService.GetOneAsync(id);
        await this._tableService.DeleteAsync(table);
        return RedirectToAction("Index");
    }
    
    // GET
    public IActionResult Create()
    {
        var table = new Table();
        return View(table);
    }
    
    // POST
    public async Task<IActionResult> CreateTable(Table table)
    {
        await this._tableService.AddAsync(table);
        return RedirectToAction("Index");
    }
    
    // GET
    public async Task<IActionResult> Edit(int id)
    {
        var table = await this._tableService.GetOneAsync(id);
        return View(table);
    }
    
    // POST
    public async Task<IActionResult> EditTable(Table table)
    {
        await this._tableService.UpdateAsync(table);
        return RedirectToAction("Index");
    }
}