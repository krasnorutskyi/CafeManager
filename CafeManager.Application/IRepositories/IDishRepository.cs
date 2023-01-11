using System.Linq.Expressions;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;

namespace CafeManager.Application.IRepositories;

public interface IDishRepository
{
    Task AddAsync(Dish entity);

    Task UpdateAsync(Dish entity);
    Task UpdateSaleAsync(Dish entity);

    Task DeleteAsync(Dish entity);

    void Attach(params object[] obj);

    Task<Dish> GetOneAsync(int id);
    
    Task<Dish> GetDishWithRelatedAsync(int id);

    Task<Dish> GetOneAsync(int id, params Expression<Func<Dish, object>>[] includeProperties);
    
    Task<IEnumerable<Dish>> GetAllAsync();

    Task<IEnumerable<Dish>> GetAllAsync(Expression<Func<Dish, bool>> predicate,
        params Expression<Func<Dish, object>>[] includeProperties);

    Task<PagedList<Dish>> GetPageAsync(PageParameters pageParameters);

    Task<PagedList<Dish>> GetPageAsync(PageParameters pageParameters,
        params Expression<Func<Dish, object>>[] includeProperties);

    Task<PagedList<Dish>> GetPageAsync(PageParameters pageParameters,
        Expression<Func<Dish, bool>> predicate,
        params Expression<Func<Dish, object>>[] includeProperties);

    Task<bool> ExistsAsync(Expression<Func<Dish, bool>> predicate);

    Task SaveAsync();
}
