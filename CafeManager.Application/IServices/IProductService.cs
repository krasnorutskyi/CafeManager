using System.Linq.Expressions;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;

namespace CafeManager.Application.IServices;

public interface IProductService
{
    Task AddAsync(Product product);
    
    Task UpdateAsync(Product product);
    
    Task DeleteAsync(Product product);
    
    Task<Product> GetOneAsync(int id);
    
    Task<Product> GetOneAsync(int id, params Expression<Func<Product, object>>[] includeProperties);
    
    Task<IEnumerable<Product>> GetAllAsync();
    
    Task<PagedList<Product>> GetPageAsync(PageParameters pageParameters);
    
    Task<PagedList<Product>> GetPageAsync(PageParameters pageParameters,
        params Expression<Func<Product, object>>[] includeProperties);

    Task<PagedList<Product>> GetPageAsync(PageParameters pageParameters,
        Expression<Func<Product, bool>> predicate,
        params Expression<Func<Product, object>>[] includeProperties);
}