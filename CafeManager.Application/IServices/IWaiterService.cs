using System.Linq.Expressions;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;

namespace CafeManager.Application.IServices;

public interface IWaiterService
{
    Task AddAsync(Waiter waiter);
    
    Task UpdateAsync(Waiter waiter);
    
    Task DeleteAsync(Waiter waiter);
    
    Task<Waiter> GetOneAsync(int id);
    
    Task<Waiter> GetOneAsync(int id, params Expression<Func<Waiter, object>>[] includeProperties);
    
    Task<PagedList<Waiter>> GetPageAsync(PageParameters pageParameters);
    
    Task<PagedList<Waiter>> GetPageAsync(PageParameters pageParameters,
        params Expression<Func<Waiter, object>>[] includeProperties);

    Task<PagedList<Waiter>> GetPageAsync(PageParameters pageParameters,
        Expression<Func<Waiter, bool>> predicate,
        params Expression<Func<Waiter, object>>[] includeProperties);
}