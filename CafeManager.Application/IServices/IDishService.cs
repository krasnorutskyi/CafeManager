using System.Linq.Expressions;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;

namespace CafeManager.Application.IServices;

public interface IDishService
{
    Task AddAsync(Dish entity);
    Task UpdateAsync(Dish entity);
    Task DeleteAsync(Dish entity);
    Task<Dish> GetOneAsync(int id);
    Task<PagedList<Dish>> GetPageAsync(PageParameters pageParameters);
    Task<PagedList<Dish>> GetPageAsync(PageParameters pageParameters, Expression<Func<Dish, bool>> predicate);
}