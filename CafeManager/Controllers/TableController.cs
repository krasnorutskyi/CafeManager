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
    private readonly IWaiterService _waiterService;

    public TableController(ITableService tableService, IWaiterService waiterService)
    {
        this._tableService = tableService;
        this._waiterService = waiterService;
    }
    
    // GET
    public async Task<IActionResult> Index(PageParameters pageParameters)
    {
        var tables = await this._tableService.GetPageAsync(pageParameters, t => t.Waiter); 
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
        tableViewModel.WaiterList = await this.PopulateWaitersDropDownList();
        return View(tableViewModel);
    }
    
    // POST
    public async Task<IActionResult> CreateTable(TableViewModel tableViewModel)
    {
        var table = new Table
        {
            PlacesNumber = tableViewModel.PlacesNumber,
            WaiterId = tableViewModel.WaiterId,
            Waiter = await this._waiterService.GetOneAsync(tableViewModel.WaiterId)
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
            WaiterId = table.WaiterId,
            WaiterList = await this.PopulateWaitersDropDownList(table.WaiterId)
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
            WaiterId = tableViewModel.WaiterId,
            Waiter = await this._waiterService.GetOneAsync(tableViewModel.WaiterId)
        };
        await this._tableService.UpdateAsync(table);
        return RedirectToAction("Index");
    }

    private async Task<IEnumerable<SelectListItem>> PopulateWaitersDropDownList(object selectedWaiter = null)
    {
        var waiters = await this._waiterService.GetAllAsync();
        return new SelectList(waiters, "Id", "LastName", selectedWaiter);
    }
}