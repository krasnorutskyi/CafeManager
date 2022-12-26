using System.Linq.Expressions;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;

namespace CafeManager.Application.IRepositories;

public interface IDishesProductsRepository<T> where T : DishesProducts
{
    Task AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);

    void Attach(params object[] obj);

    Task<T> GetOneAsync(int idDish, int orderNumber);

    Task<PagedList<T>> GetPageAsync(PageParameters pageParameters);
    
    Task<PagedList<T>> GetPageAsync(PageParameters pageParameters, Expression<Func<T, bool>> predicate);

    Task SaveAsync();
}