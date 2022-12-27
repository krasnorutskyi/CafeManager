using System.Linq.Expressions;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;

namespace CafeManager.Application.IRepositories;

public interface IDishesOrdersRepository
{
    Task AddAsync(DishesOrders entity);

    Task UpdateAsync(DishesOrders entity);

    Task DeleteAsync(DishesOrders entity);

    void Attach(params object[] obj);

    Task<DishesOrders> GetOneAsync(int idDish, int orderNumber);

    Task<PagedList<DishesOrders>> GetPageAsync(PageParameters pageParameters);

    Task<PagedList<DishesOrders>> GetPageAsync(PageParameters pageParameters, Expression<Func<DishesOrders, bool>> predicate);
}