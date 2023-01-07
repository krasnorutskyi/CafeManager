using CafeManager.Application.IServices;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;
using CafeManager.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CafeManager.Controllers;

public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    private readonly IWaiterService _waiterService;
    private readonly IDishService _dishService;
    private readonly IDishesOrdersService _dishesOrdersService;

    public OrderController(IOrderService orderService, IWaiterService waiterService, IDishService dishService, IDishesOrdersService dishesOrdersService)
    {
        this._orderService = orderService;
        this._waiterService = waiterService;
        this._dishService = dishService;
        this._dishesOrdersService = dishesOrdersService;
    }
    // GET
    public async Task<IActionResult> Index(PageParameters pageParameters)
    {
        var orders = await this._orderService.GetPageAsync(pageParameters);
        return View(orders);
    }
    
    
    
    // GET
    public async Task<IActionResult> Create()
    {
        var orderViewModel = new OrderViewModel();
        orderViewModel.WaiterList = await this.PopulateWaitersDropDownList();
        orderViewModel.Dishes = (List<Dish>) await this._dishService.GetAllAsync();
        return View(orderViewModel);
    }
    
    // POST
    [HttpPost]
    public async Task<IActionResult> Create(OrderViewModel orderViewModel)
    {
        
        var order = new Order
        {
            Date = DateTime.Today,
            Tipp = orderViewModel.Tipp,
            HasClientsSale = orderViewModel.HasClientsSale,
            WaiterId = orderViewModel.WaiterId,
            Waiter = await this._waiterService.GetOneAsync(orderViewModel.WaiterId),
            DishesOrders = new List<DishesOrders>()
        };
        var price = 0.0;
        foreach (var d in orderViewModel.DishesOrdersList)
        {
            d.Dish = await this._dishService.GetOneAsync(d.DishId);
            d.DishName = d.Dish.Name;
            d.DishesTotal = d.Dish.Price * d.DishesAmount;
            price += d.DishesTotal;
            order.DishesOrders.Add(d);
        }
        if (orderViewModel.HasClientsSale)
        {
            price *= 0.95;
        }
        order.Price = (float)price;
        order.VAT = (float)(price * 0.2);
        await this._orderService.AddAsync(order);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Details(int id)
    {
        var order = await this._orderService.GetOneAsync(id, o=> o.Waiter, o => o.DishesOrders);
        return View(order);
    }
    
    
    private async Task<IEnumerable<SelectListItem>> PopulateWaitersDropDownList(object selectedWaiter = null)
    {
        var waiters = await this._waiterService.GetAllAsync();
        return new SelectList(waiters, "Id", "LastName", selectedWaiter);
    }
    
    // GET
    /*public async Task<IActionResult> Edit(int id)
         {
             var order = await this._orderService.GetOneAsync(id);
             var orderViewModel = new OrderViewModel
             {
                 Date = default,
                 Price = 0,
                 Tipp = 0,
                 HasClientsSale = false,
                 VAT = 0,
                 WaiterId = 0,
                 WaiterList = null
             };
             return View(orderViewModel);
         }
         
         // POST
         [HttpPost]
         public async Task<IActionResult> Edit(OrderViewModel orderViewModel)
         {
             var order = new Order
             {
                 Id = 0,
                 Date = default,
                 Price = 0,
                 Tipp = 0,
                 HasClientsSale = false,
                 VAT = 0,
                 WaiterId = 0,
                 Waiter = null,
                 DishesOrders = null
             };
             await this._orderService.UpdateAsync(order);
             return RedirectToAction("Index");
         }*/
    
    // public async Task<IActionResult> Delete(int id)
    // {
    //     var order = await this._orderService.GetOneAsync(id);
    //     await this._orderService.DeleteAsync(order);
    //     return RedirectToAction("Index");
    // }
}