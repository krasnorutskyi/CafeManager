using System.Linq.Expressions;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;

namespace CafeManager.Application.IServices;

public interface IDishService
{
    Task AddAsync(Dish dish);
    
    Task UpdateAsync(Dish dish);
    
    Task UpdateSaleAsync(Dish entity);
    
    Task DeleteAsync(Dish dish);

    Task<List<Dish>> GetDishOfTheDay();

    Task<Dish> GetOneAsync(int id);
    Task<Dish> GetDishWithRelatedAsync(int id);
    
    Task<IEnumerable<Dish>> GetAllAsync();
    
    Task<Dish> GetOneAsync(int id, params Expression<Func<Dish, object>>[] includeProperties);
    
    Task<PagedList<Dish>> GetPageAsync(PageParameters pageParameters);
    Task<PagedList<Dish>> GetPageAsync(PageParameters pageParameters, string searchString, int categoryId);
    
    Task<PagedList<Dish>> GetPageAsync(PageParameters pageParameters,
        params Expression<Func<Dish, object>>[] includeProperties);

    Task<PagedList<Dish>> GetPageAsync(PageParameters pageParameters,
        Expression<Func<Dish, bool>> predicate,
        params Expression<Func<Dish, object>>[] includeProperties);
}