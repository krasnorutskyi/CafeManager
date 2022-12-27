using System.Linq.Expressions;
using CafeManager.Application.IRepositories;
using CafeManager.Application.IServices;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;

namespace CafeManager.Infrastructure.Services;

public class DishesProductsService : IDishesProductsService
{
    private readonly IDishesProductsRepository _dishesProductsRepository;

    public DishesProductsService(IDishesProductsRepository dishesProductsRepository)
    {
        this._dishesProductsRepository = dishesProductsRepository;
    }


    public async Task AddAsync(DishesProducts dishesProducts)
    {
        this._dishesProductsRepository.Attach(dishesProducts);
        await this._dishesProductsRepository.AddAsync(dishesProducts);
    }

    public async Task UpdateAsync(DishesProducts dishesProducts)
    {
        this._dishesProductsRepository.Attach(dishesProducts);
        await this._dishesProductsRepository.UpdateAsync(dishesProducts);
    }

    public async Task DeleteAsync(DishesProducts dishesProducts)
    {
        await this._dishesProductsRepository.DeleteAsync(dishesProducts);
    }

    public async Task<DishesProducts> GetOneAsync(int dishId, int productId)
    {
        return await this._dishesProductsRepository.GetOneAsync(dishId, productId);
    }

    public async Task<PagedList<DishesProducts>> GetPageAsync(PageParameters pageParameters)
    {
        return await this._dishesProductsRepository.GetPageAsync(pageParameters);
    }

    public async Task<PagedList<DishesProducts>> GetPageAsync(PageParameters pageParameters, Expression<Func<DishesProducts, bool>> predicate)
    {
        return await this._dishesProductsRepository.GetPageAsync(pageParameters, predicate);
    }
}