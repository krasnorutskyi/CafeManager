using System.Linq.Expressions;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;

namespace CafeManager.Application.IServices;

public interface IOrderService
{
    Task AddAsync(Order entity);
    Task UpdateAsync(Order entity);
    Task DeleteAsync(Order entity);
    Task<Order> GetOneAsync(int id);
    Task<PagedList<Order>> GetPageAsync(PageParameters pageParameters);
    Task<PagedList<Order>> GetPageAsync(PageParameters pageParameters, Expression<Func<Order, bool>> predicate);
    void GetSalesReport();
}