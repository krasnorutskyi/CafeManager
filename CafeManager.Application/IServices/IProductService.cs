using System.Linq.Expressions;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;

namespace CafeManager.Application.IServices;

public interface IProductService
{
    Task AddAsync(Product entity);
    Task UpdateAsync(Product entity);
    Task DeleteAsync(Product entity);
    Task<Product> GetOneAsync(int id);
    Task<PagedList<Product>> GetPageAsync(PageParameters pageParameters);
    Task<PagedList<Product>> GetPageAsync(PageParameters pageParameters, Expression<Func<Product, bool>> predicate);
}