using System.Linq.Expressions;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;

namespace CafeManager.Application.IServices;

public interface IDishesProductsService
{
    Task AddAsync(DishesProducts dishesProducts);
    
    Task UpdateAsync(DishesProducts dishesProducts);
    
    Task DeleteAsync(DishesProducts dishesProducts);
    
    Task<DishesProducts> GetOneAsync(int dishId, int productId);

    Task<PagedList<DishesProducts>> GetPageAsync(PageParameters pageParameters);

    Task<PagedList<DishesProducts>> GetPageAsync(PageParameters pageParameters,
        Expression<Func<DishesProducts, bool>> predicate);
}