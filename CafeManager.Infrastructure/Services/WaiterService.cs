using System.Linq.Expressions;
using CafeManager.Application.IRepositories;
using CafeManager.Application.IServices;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;

namespace CafeManager.Infrastructure.Services;

public class WaiterService : IWaiterService
{
    private readonly IGenericRepository<Waiter> _waiterRepository;

    public WaiterService(IGenericRepository<Waiter> waiterRepository)
    {
        this._waiterRepository = waiterRepository;
    }
    
    public async Task AddAsync(Waiter waiter)
    {
        this._waiterRepository.Attach(waiter);
        await this._waiterRepository.AddAsync(waiter);
    }

    public async Task UpdateAsync(Waiter waiter)
    {
        this._waiterRepository.Attach(waiter);
        await this._waiterRepository.UpdateAsync(waiter);
    }

    public async Task DeleteAsync(Waiter waiter)
    {
        await this._waiterRepository.DeleteAsync(waiter);
    }

    public async Task<Waiter> GetOneAsync(int id)
    {
        return await this._waiterRepository.GetOneAsync(id);
    }

    public async Task<IEnumerable<Waiter>> GetAllAsync()
    {
        return await this._waiterRepository.GetAllAsync();
    }

    public async Task<Waiter> GetOneAsync(int id, params Expression<Func<Waiter, object>>[] includeProperties)
    {
        return await this._waiterRepository.GetOneAsync(id, includeProperties);
    }

    public async Task<PagedList<Waiter>> GetPageAsync(PageParameters pageParameters)
    {
        return await this._waiterRepository.GetPageAsync(pageParameters);
    }

    public async Task<PagedList<Waiter>> GetPageAsync(PageParameters pageParameters, params Expression<Func<Waiter, object>>[] includeProperties)
    {
        return await this._waiterRepository.GetPageAsync(pageParameters, includeProperties);
    }

    public async Task<PagedList<Waiter>> GetPageAsync(PageParameters pageParameters, Expression<Func<Waiter, bool>> predicate, params Expression<Func<Waiter, object>>[] includeProperties)
    {
        return await this._waiterRepository.GetPageAsync(pageParameters, predicate, includeProperties);
    }
}