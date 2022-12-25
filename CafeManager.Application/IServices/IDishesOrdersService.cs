using System.Linq.Expressions;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;

namespace CafeManager.Application.IServices;

public interface IDishesOrdersService
{
    Task AddAsync(DishesOrders dishesOrders);
    
    Task UpdateAsync(DishesOrders dishesOrders);
    
    Task DeleteAsync(DishesOrders dishesOrders);
    
    Task<DishesOrders> GetOneAsync(int id);
    
    Task<DishesOrders> GetOneAsync(int id, params Expression<Func<DishesOrders, object>>[] includeProperties);
    
    Task<PagedList<DishesOrders>> GetPageAsync(PageParameters pageParameters);
    
    Task<PagedList<DishesOrders>> GetPageAsync(PageParameters pageParameters,
        params Expression<Func<DishesOrders, object>>[] includeProperties);

    Task<PagedList<DishesOrders>> GetPageAsync(PageParameters pageParameters,
        Expression<Func<DishesOrders, bool>> predicate,
        params Expression<Func<DishesOrders, object>>[] includeProperties);
}