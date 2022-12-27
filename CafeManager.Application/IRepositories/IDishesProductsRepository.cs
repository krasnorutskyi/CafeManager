using System.Linq.Expressions;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;

namespace CafeManager.Application.IRepositories;

public interface IDishesProductsRepository
{
    Task AddAsync(DishesProducts entity);

    Task UpdateAsync(DishesProducts entity);

    Task DeleteAsync(DishesProducts entity);

    void Attach(params object[] obj);

    Task<DishesProducts> GetOneAsync(int idDish, int orderNumber);

    Task<PagedList<DishesProducts>> GetPageAsync(PageParameters pageParameters);

    Task<PagedList<DishesProducts>> GetPageAsync(PageParameters pageParameters, Expression<Func<DishesProducts, bool>> predicate);
}