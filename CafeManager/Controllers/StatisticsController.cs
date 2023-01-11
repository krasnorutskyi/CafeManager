using CafeManager.Application.IServices;
using CafeManager.Application.Paging;
using Microsoft.AspNetCore.Mvc;

namespace CafeManager.Controllers;

public class StatisticsController : Controller
{

    private readonly IStatisticsService _statisticsService;
    private readonly IOrderService _orderService;

    public StatisticsController(IStatisticsService statisticsService, IOrderService orderService)
    {
        this._statisticsService = statisticsService;
        this._orderService = orderService;
    }
    
    // GET
    public IActionResult Index()
    {
        return View();
    }
    
    public async Task<IActionResult> GetDishes(PageParameters pageParameters)
    {
        var dishes = await _statisticsService.GetDishesStatisticsAsync(pageParameters);
        return View(dishes);
    }
    
    public async Task<IActionResult> GetLeastBusyDay(PageParameters pageParameters)
    {
        var day = await _statisticsService.GetLeastBusyDayStatisticsAsync();
        var orders = await this._orderService.GetPageAsync(pageParameters, o => o.Date == day);
        return View(orders);
    }
    
    public async Task<IActionResult> GetWaiters(PageParameters pageParameters)
    {
        var waiters = await this._statisticsService.GetWaitersStatisticsAsync(pageParameters);
        return View(waiters);
    }
    
    public async Task<IActionResult> GetTable(PageParameters pageParameters)
    {
        var tables = await this._statisticsService.GetTablesStatisticsAsync(pageParameters);
        return View(tables);
    }
}