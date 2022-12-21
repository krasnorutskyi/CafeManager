using System.Linq.Expressions;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;

namespace CafeManager.Application.IServices;

public interface ICategoryService
{
    Task AddAsync(Category entity);
    Task UpdateAsync(Category entity);
    Task DeleteAsync(Category entity);
    Task<Category> GetOneAsync(int id);
    Task<PagedList<Category>> GetPageAsync(PageParameters pageParameters);
    Task<PagedList<Category>> GetPageAsync(PageParameters pageParameters, Expression<Func<Category, bool>> predicate);
}