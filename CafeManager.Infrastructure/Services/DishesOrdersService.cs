using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using CafeManager.Application.IRepositories;
using CafeManager.Application.IServices;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;

namespace CafeManager.Infrastructure.Services;

public class DishesOrdersService : IDishesOrdersService
{
    private readonly IDishesOrdersRepository<DishesOrders> _dishesOrdersRepository;
    
    public DishesOrdersService(IDishesOrdersRepository<DishesOrders> dishesOrdersRepository)
    {
        this._dishesOrdersRepository = dishesOrdersRepository;
    }


    public async Task AddAsync(DishesOrders dishesOrders)
    {
        this._dishesOrdersRepository.Attach(dishesOrders);
        await this._dishesOrdersRepository.AddAsync(dishesOrders);
    }

    public async Task UpdateAsync(DishesOrders dishesOrders)
    {
        this._dishesOrdersRepository.Attach(dishesOrders);
        await this._dishesOrdersRepository.UpdateAsync(dishesOrders);
    }

    public async Task DeleteAsync(DishesOrders dishesOrders)
    {
        await this._dishesOrdersRepository.DeleteAsync(dishesOrders);
    }

    public async Task<DishesOrders> GetOneAsync(int dishId, int ordersNumber)
    {
        return await this._dishesOrdersRepository.GetOneAsync(dishId, ordersNumber);
    }

    public async Task<PagedList<DishesOrders>> GetPageAsync(PageParameters pageParameters)
    {
        return await this._dishesOrdersRepository.GetPageAsync(pageParameters);
    }

    public async Task<PagedList<DishesOrders>> GetPageAsync(PageParameters pageParameters, Expression<Func<DishesOrders, bool>> predicate)
    {
        return await this._dishesOrdersRepository.GetPageAsync(pageParameters, predicate);
    }
}