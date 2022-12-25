using System.Linq.Expressions;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;

namespace CafeManager.Application.IServices;

public interface ICategoryService
{
    Task AddAsync(Category category);
    
    Task UpdateAsync(Category category);
    
    Task DeleteAsync(Category category);
    
    Task<Category> GetOneAsync(int id);
    
    Task<Category> GetOneAsync(int id, params Expression<Func<Category, object>>[] includeProperties);
    
    Task<PagedList<Category>> GetPageAsync(PageParameters pageParameters);
    
    Task<PagedList<Category>> GetPageAsync(PageParameters pageParameters,
        params Expression<Func<Category, object>>[] includeProperties);

    Task<PagedList<Category>> GetPageAsync(PageParameters pageParameters,
        Expression<Func<Category, bool>> predicate,
        params Expression<Func<Category, object>>[] includeProperties);
}