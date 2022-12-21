using System.Linq.Expressions;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;

namespace CafeManager.Application.IServices;

public interface IDishesProductsService
{
    Task AddAsync(DishesProducts entity);
    Task UpdateAsync(DishesProducts entity);
    Task DeleteAsync(DishesProducts entity);
    Task<DishesProducts> GetOneAsync(int id);
    Task<PagedList<DishesProducts>> GetPageAsync(PageParameters pageParameters);
    Task<PagedList<DishesProducts>> GetPageAsync(PageParameters pageParameters, Expression<Func<DishesProducts, bool>> predicate);
}