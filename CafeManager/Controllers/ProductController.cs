using CafeManager.Application.IServices;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;
using CafeManager.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CafeManager.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        this._productService = productService;
    }
    
    // GET
    public async Task<IActionResult> Index(PageParameters pageParameters, PagedList<Product> products)
    {
        products = await this._productService.GetPageAsync(pageParameters);

        return View(products);
    }
    
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await this._productService.GetOneAsync(id);
        await this._productService.DeleteAsync(product);
        return RedirectToAction("Index");
    }
    
    // GET
    public IActionResult Create()
    {
        var product = new Product();
        return View(product);
    }
    
    // POST
    public async Task<IActionResult> CreateProduct(Product product)
    {
        await this._productService.AddAsync(product);
        return RedirectToAction("Index");
    }
    
    // GET
    public async Task<IActionResult> Edit(int id)
    {
        var product = await this._productService.GetOneAsync(id);
        return View(product);
    }
    
    // POST
    public async Task<IActionResult> EditProduct(Product product)
    {
        await this._productService.UpdateAsync(product);
        return RedirectToAction("Index");
    }
}