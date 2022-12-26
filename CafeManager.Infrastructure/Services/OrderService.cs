using System.Linq.Expressions;
using CafeManager.Application.IRepositories;
using CafeManager.Application.IServices;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;

namespace CafeManager.Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly IGenericRepository<Order> _orderRepository;

    public OrderService(IGenericRepository<Order> orderRepository)
    {
        this._orderRepository = _orderRepository;
    }

    public async Task AddAsync(Order order)
    {
        this._orderRepository.Attach(order);
        await this._orderRepository.AddAsync(order);
    }

    public async Task UpdateAsync(Order order)
    {
        this._orderRepository.Attach(order);
        await this._orderRepository.UpdateAsync(order);
    }

    public async Task DeleteAsync(Order order)
    {
        await this._orderRepository.DeleteAsync(order);
    }

    public async Task<Order> GetOneAsync(int id)
    {
        return await this._orderRepository.GetOneAsync(id);
    }

    public async Task<Order> GetOneAsync(int id, params Expression<Func<Order, object>>[] includeProperties)
    {
        return await this._orderRepository.GetOneAsync(id, includeProperties);
    }

    public async Task<PagedList<Order>> GetPageAsync(PageParameters pageParameters)
    {
        return await this._orderRepository.GetPageAsync(pageParameters);
    }

    public async Task<PagedList<Order>> GetPageAsync(PageParameters pageParameters, params Expression<Func<Order, object>>[] includeProperties)
    {
        return await this._orderRepository.GetPageAsync(pageParameters, includeProperties);
    }

    public async Task<PagedList<Order>> GetPageAsync(PageParameters pageParameters, Expression<Func<Order, bool>> predicate, params Expression<Func<Order, object>>[] includeProperties)
    {
        return await this._orderRepository.GetPageAsync(pageParameters, predicate, includeProperties);
    }
}