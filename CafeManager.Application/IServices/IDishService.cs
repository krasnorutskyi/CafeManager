using System.Linq.Expressions;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;

namespace CafeManager.Application.IServices;

public interface IDishService
{
    Task AddAsync(Dish dish);
    
    Task UpdateAsync(Dish dish);
    
    Task DeleteAsync(Dish dish);
    
    Task<Dish> GetOneAsync(int id);
    
    Task<IEnumerable<Dish>> GetAllAsync();
    
    Task<Dish> GetOneAsync(int id, params Expression<Func<Dish, object>>[] includeProperties);
    
    Task<PagedList<Dish>> GetPageAsync(PageParameters pageParameters);
    
    Task<PagedList<Dish>> GetPageAsync(PageParameters pageParameters,
        params Expression<Func<Dish, object>>[] includeProperties);

    Task<PagedList<Dish>> GetPageAsync(PageParameters pageParameters,
        Expression<Func<Dish, bool>> predicate,
        params Expression<Func<Dish, object>>[] includeProperties);
}