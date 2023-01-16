using System.Diagnostics;
using CafeManager.Application.IServices;
using Microsoft.AspNetCore.Mvc;
using CafeManager.Models;

namespace CafeManager.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IOrderService _orderService;
    private readonly IDishService _dishService;

    public HomeController(ILogger<HomeController> logger, IOrderService orderService, IDishService dishService)
    {
        _logger = logger;
        this._orderService = orderService;
        this._dishService = dishService;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    public async Task<IActionResult> GetDish()
    {
        var dishes = await this._dishService.GetDishOfTheDay();
        return View(dishes);
    }
    
    [HttpGet]
    public IActionResult GetSales()
    {
        var date = new DateTime();
        date = DateTime.Today;
        return View(date);
    }
    
    [HttpPost]
    public async Task<IActionResult> GetSales(DateTime date)
    { 
        var pdf = await this._orderService.GenerateSalesReport(date);
        var file = new FileContentResult(pdf, "application/pdf");
        return file;
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}