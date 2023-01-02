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

    public OrderController(IOrderService orderService, IWaiterService waiterService)
    {
        this._orderService = orderService;
        this._waiterService = waiterService;
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
        return View(orderViewModel);
    }
    
    // POST
    [HttpPost]
    public async Task<IActionResult> Create(OrderViewModel orderViewModel)
    {
        if (orderViewModel.HasClientsSale)
        {
            orderViewModel.Price = (float)(orderViewModel.Price * 0.95);
        }
        var order = new Order
        {
            Date = orderViewModel.Date,
            Price = orderViewModel.Price,
            Tipp = orderViewModel.Tipp,
            HasClientsSale = orderViewModel.HasClientsSale,
            VAT = (float)(orderViewModel.Price * 0.2),
            WaiterId = orderViewModel.WaiterId,
            Waiter = await this._waiterService.GetOneAsync(orderViewModel.WaiterId)
        };
        await this._orderService.AddAsync(order);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Details(int id)
    {
        var order = await this._orderService.GetOneAsync(id, o=> o.Waiter);
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