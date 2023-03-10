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
    private readonly IProductService _productService;
    private readonly ITableService _tableService;

    public OrderController(IOrderService orderService, IWaiterService waiterService, IDishService dishService, IProductService productService, ITableService tableService)
    {
        this._orderService = orderService;
        this._waiterService = waiterService;
        this._dishService = dishService;
        this._productService = productService;
        this._tableService = tableService;
    }
    // GET
    public async Task<IActionResult> Index(PageParameters pageParameters)
    {
        var orders = await this._orderService.GetPageAsync(pageParameters);
        return View(orders);
    }

    public async Task<IActionResult> GenerateInvoice(int id)
    {
        var pdf = await this._orderService.GenerateInvoice(id);
        var file = new FileContentResult(pdf, "application/pdf");
        return file;
    }


    // GET
    [HttpGet]
    public async Task<IActionResult> Create(OrderViewModel orderViewModel)
    {
        orderViewModel.WaiterList = await this.PopulateWaitersDropDownList();
        orderViewModel.Dishes = (List<Dish>) await this._dishService.GetAllAsync();
        orderViewModel.TableList = await this.PopulateTablesDropDownList();
        ViewBag.Message = orderViewModel.DishException;
        return View(orderViewModel);
    }
    
    // POST
    [HttpPost]
    public async Task<IActionResult> CreateOrder(OrderViewModel orderViewModel)
    {
        
        var order = new Order
        {
            Date = DateTime.Today,
            Tipp = orderViewModel.Tipp,
            HasClientsSale = orderViewModel.HasClientsSale,
            WaiterId = orderViewModel.WaiterId,
            Waiter = await this._waiterService.GetOneAsync(orderViewModel.WaiterId),
            TableId = orderViewModel.TableId,
            Table = await this._tableService.GetOneAsync(orderViewModel.TableId),
            DishesOrders = new List<DishesOrders>()
        };
        var price = 0.0;
        bool isEnough = true;
        foreach (var d in orderViewModel.DishesOrdersList)
        {
            d.Dish = await this._dishService.GetOneAsync(d.DishId);
            var dish = await this._dishService.GetDishWithRelatedAsync(d.DishId);
            foreach (var p in dish.DishesProducts)
            {
                var product = await this._productService.GetOneAsync(p.ProductId);
                var productExspence = p.ProductsAmount * d.DishesAmount;
                if (product.Quantity < productExspence)
                {
                    var str = $"There is no required quantity of products to makethis amount of {d.Dish.Name}";
                    orderViewModel.DishException = str;
                    return RedirectToAction("Create", orderViewModel);
                }
                else
                {
                    product.Quantity -= productExspence;
                    await this._productService.UpdateAsync(product);
                }
            }
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
        
        foreach (var o in order.DishesOrders)
        {
            var dish = await this._dishService.GetOneAsync(o.DishId);
            dish.Sales += o.DishesAmount;
            await this._dishService.UpdateSaleAsync(dish);
        }
        
        await this._orderService.AddAsync(order);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Details(int id)
    {
        var order = await this._orderService.GetOneAsync(id, o=> o.Waiter, o => o.DishesOrders, o=> o.Table);
        return View(order);
    }
    
    
    private async Task<IEnumerable<SelectListItem>> PopulateWaitersDropDownList(object selectedWaiter = null)
    {
        var waiters = await this._waiterService.GetAllAsync();
        return new SelectList(waiters, "Id", "LastName", selectedWaiter);
    }
    
    private async Task<IEnumerable<SelectListItem>> PopulateTablesDropDownList(object selectedTable = null)
    {
        var tables = await this._tableService.GetAllAsync();
        return new SelectList(tables, "Id", "Id", selectedTable);
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