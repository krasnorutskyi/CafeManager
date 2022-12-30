using CafeManager.Application.IServices;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CafeManager.Controllers;

public class WaiterController : Controller
{
    private readonly IWaiterService _waiterService;

    public WaiterController(IWaiterService waiterService)
    {
        this._waiterService = waiterService;
    }
    
    // GET
    public async Task<IActionResult> Index(PageParameters pageParameters)
    {
        var waiters = await this._waiterService.GetPageAsync(pageParameters);
        return View(waiters);
    }

    public async Task<IActionResult> DeleteWaiter(int id)
    {
        var waiter = await this._waiterService.GetOneAsync(id);
        await this._waiterService.DeleteAsync(waiter);
        return RedirectToAction("Index");
    }
    
    // GET
    public IActionResult Create()
    {
        var waiter = new Waiter();
        return View(waiter);
    }
    
    // POST
    public async Task<IActionResult> CreateWaiter(Waiter waiter)
    {
        await this._waiterService.AddAsync(waiter);
        return RedirectToAction("Index");
    }
    
    // GET
    public async Task<IActionResult> Edit(int id)
    {
        var waiter = await this._waiterService.GetOneAsync(id);
        return View(waiter);
    }
    
    // POST
    public async Task<IActionResult> EditWaiter(Waiter waiter)
    {
        await this._waiterService.UpdateAsync(waiter);
        return RedirectToAction("Index");
    }
}