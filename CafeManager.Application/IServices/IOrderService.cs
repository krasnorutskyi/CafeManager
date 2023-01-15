using System.Linq.Expressions;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;

namespace CafeManager.Application.IServices;

public interface IOrderService
{
    Task AddAsync(Order order);
    
    Task UpdateAsync(Order order);
    
    Task DeleteAsync(Order order);
    
    Task<Order> GetOneAsync(int id);

    Task<byte[]> GenerateInvoice(int id);
    
    Task<byte[]> GenerateSalesReport(DateTime date);

    Task<Order> GetOneAsync(int id, params Expression<Func<Order, object>>[] includeProperties);
    
    Task<PagedList<Order>> GetPageAsync(PageParameters pageParameters);
    
    Task<PagedList<Order>> GetPageAsync(PageParameters pageParameters,
        params Expression<Func<Order, object>>[] includeProperties);

    Task<PagedList<Order>> GetPageAsync(PageParameters pageParameters,
        Expression<Func<Order, bool>> predicate,
        params Expression<Func<Order, object>>[] includeProperties);
}