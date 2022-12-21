using System.Linq.Expressions;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;

namespace CafeManager.Application.IServices;

public interface IWaiterService
{
    Task AddAsync(Waiter entity);
    Task UpdateAsync(Waiter entity);
    Task DeleteAsync(Waiter entity);
    Task<Waiter> GetOneAsync(int id);
    Task<PagedList<Waiter>> GetPageAsync(PageParameters pageParameters);
    Task<PagedList<Waiter>> GetPageAsync(PageParameters pageParameters, Expression<Func<Waiter, bool>> predicate);
}