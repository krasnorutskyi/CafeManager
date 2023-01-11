using CafeManager.Application.IServices;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;
using CafeManager.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
    public async Task<IActionResult> Create()
    {
        var tableViewModel = new TableViewModel();
        return View(tableViewModel);
    }
    
    // POST
    public async Task<IActionResult> CreateTable(TableViewModel tableViewModel)
    {
        var table = new Table
        {
            PlacesNumber = tableViewModel.PlacesNumber,
        };
        await this._tableService.AddAsync(table);
        return RedirectToAction("Index");
    }
    
    // GET
    public async Task<IActionResult> Edit(int id)
    {
        var table = await this._tableService.GetOneAsync(id);
        var tableViewModel = new TableViewModel
        {
            Id = table.Id,
            PlacesNumber = table.PlacesNumber,
        };
        return View(tableViewModel);
    }
    
    // POST
    public async Task<IActionResult> EditTable(TableViewModel tableViewModel)
    {
        var table = new Table()
        {
            Id = tableViewModel.Id,
            PlacesNumber = tableViewModel.PlacesNumber,
        };
        await this._tableService.UpdateAsync(table);
        return RedirectToAction("Index");
    }
}