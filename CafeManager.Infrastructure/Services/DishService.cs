using System.Linq.Expressions;
using CafeManager.Application.IRepositories;
using CafeManager.Application.IServices;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;
using CafeManager.Infrastructure.Repositories;

namespace CafeManager.Infrastructure.Services;

public class DishService : IDishService
{
    private readonly IDishRepository _dishRepository;

    public DishService(IDishRepository dishRepository)
    {
        this._dishRepository = dishRepository;
    }

    public async Task AddAsync(Dish dish)
    {
        this._dishRepository.Attach(dish);
        await this._dishRepository.AddAsync(dish);
    }

    public async Task UpdateSaleAsync(Dish entity)
    {
        await this._dishRepository.UpdateSaleAsync(entity);
    }

    public async Task UpdateAsync(Dish dish)
    {
        
        await this._dishRepository.UpdateAsync(dish);
    }

    public async Task DeleteAsync(Dish dish)
    {
        await this._dishRepository.DeleteAsync(dish);
    }

    public async Task<Dish> GetOneAsync(int id)
    {
        return await this._dishRepository.GetOneAsync(id);
    }

    public async Task<Dish> GetDishWithRelatedAsync(int id)
    {
        return await this._dishRepository.GetDishWithRelatedAsync(id);
    }

    public async Task<Dish> GetOneAsync(int id, params Expression<Func<Dish, object>>[] includeProperties)
    {
        return await this._dishRepository.GetOneAsync(id, includeProperties);
    }

    public async Task<IEnumerable<Dish>> GetAllAsync()
    {
        return await this._dishRepository.GetAllAsync();
    }

    public async Task<PagedList<Dish>> GetPageAsync(PageParameters pageParameters)
    {
        return await this._dishRepository.GetPageAsync(pageParameters);
    }

    public async Task<PagedList<Dish>> GetPageAsync(PageParameters pageParameters, params Expression<Func<Dish, object>>[] includeProperties)
    {
        return await this._dishRepository.GetPageAsync(pageParameters, includeProperties);
    }

    public async Task<PagedList<Dish>> GetPageAsync(PageParameters pageParameters, Expression<Func<Dish, bool>> predicate, params Expression<Func<Dish, object>>[] includeProperties)
    {
        return await this._dishRepository.GetPageAsync(pageParameters, predicate, includeProperties);
    }
}