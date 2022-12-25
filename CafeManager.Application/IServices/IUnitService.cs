using System.Linq.Expressions;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;

namespace CafeManager.Application.IServices;

public interface IUnitService
{
    Task AddAsync(Unit unit);
    
    Task UpdateAsync(Unit unit);
    
    Task DeleteAsync(Unit unit);
    
    Task<Unit> GetOneAsync(int id);
    
    Task<Unit> GetOneAsync(int id, params Expression<Func<Unit, object>>[] includeProperties);
    
    Task<PagedList<Unit>> GetPageAsync(PageParameters pageParameters);
    
    Task<PagedList<Unit>> GetPageAsync(PageParameters pageParameters,
        params Expression<Func<Unit, object>>[] includeProperties);

    Task<PagedList<Unit>> GetPageAsync(PageParameters pageParameters,
        Expression<Func<Unit, bool>> predicate,
        params Expression<Func<Unit, object>>[] includeProperties);
}