using System.Linq.Expressions;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;

namespace CafeManager.Application.IServices;

public interface ITableService
{
    Task AddAsync(Table table);
    
    Task UpdateAsync(Table table);
    
    Task DeleteAsync(Table table);
    
    Task<Table> GetOneAsync(int id);
    
    Task<Table> GetOneAsync(int id, params Expression<Func<Table, object>>[] includeProperties);
    
    Task<PagedList<Table>> GetPageAsync(PageParameters pageParameters);
    
    Task<PagedList<Table>> GetPageAsync(PageParameters pageParameters,
        params Expression<Func<Table, object>>[] includeProperties);

    Task<PagedList<Table>> GetPageAsync(PageParameters pageParameters,
        Expression<Func<Table, bool>> predicate,
        params Expression<Func<Table, object>>[] includeProperties);
}