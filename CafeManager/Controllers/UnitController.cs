using CafeManager.Application.IServices;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CafeManager.Controllers;

public class UnitController : Controller
{
    private readonly IUnitService _unitService;

    public UnitController(IUnitService unitService)
    {
        this._unitService = unitService;
    }
    
    // GET
    public async Task<IActionResult> Index(PageParameters pageParameters)
    {
        var units = await this._unitService.GetPageAsync(pageParameters);
        return View(units);
    }

    public async Task<IActionResult> DeleteUnit(int id)
    {
        var unit = await this._unitService.GetOneAsync(id);
        await this._unitService.DeleteAsync(unit);
        return RedirectToAction("Index");
    }
    
    // GET
    public IActionResult Create()
    {
        var unit = new Unit();
        return View(unit);
    }
    
    // POST
    public async Task<IActionResult> CreateUnit(Unit unit)
    {
        await this._unitService.AddAsync(unit);
        return RedirectToAction("Index");
    }
    
    // GET
    public async Task<IActionResult> Edit(int id)
    {
        var unit = await this._unitService.GetOneAsync(id);
        return View(unit);
    }
    
    // POST
    public async Task<IActionResult> EditUnit(Unit unit)
    {
        await this._unitService.UpdateAsync(unit);
        return RedirectToAction("Index");
    }
}