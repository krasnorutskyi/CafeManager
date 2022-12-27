using System.Linq.Expressions;
using CafeManager.Application.IRepositories;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;
using CafeManager.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace CafeManager.Infrastructure.Repositories;

public class DishesOrdersRepository : IDishesOrdersRepository
{
    private readonly ApplicationContext _db;
    private readonly DbSet<DishesOrders> _table;

    public DishesOrdersRepository(ApplicationContext context)
    {
        this._db = context;
        this._table = _db.Set<DishesOrders>();
    }

    public async Task AddAsync(DishesOrders entity)
    {
        await this._table.AddAsync(entity);
        await this.SaveAsync();
    }

    public async Task UpdateAsync(DishesOrders entity)
    {
        this._table.Update(entity);
        await this.SaveAsync();
    }

    public async Task DeleteAsync(DishesOrders entity)
    {
        this._table.Remove(entity);
        await this.SaveAsync();
    }

    public void Attach(params object[] obj)
    {
        this._db.AttachRange(obj);
    }

    public async Task<DishesOrders> GetOneAsync(int dishId, int ordersNumber)
    {
        return await this._table.FirstOrDefaultAsync<DishesOrders>(entity => entity.DishId == dishId && entity.OrdersNumber == ordersNumber );
    }

    public async Task<PagedList<DishesOrders>> GetPageAsync(PageParameters pageParameters)
    {
        var items = this._table.AsNoTracking()
            .Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
            .Take(pageParameters.PageSize);
        var pagedList = new PagedList<DishesOrders>(items, pageParameters, await items.CountAsync());
        return pagedList;
    }

    public async Task<PagedList<DishesOrders>> GetPageAsync(PageParameters pageParameters, Expression<Func<DishesOrders, bool>> predicate)
    {
        var items = this._table.AsNoTracking()
            .Where(predicate)
            .Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
            .Take(pageParameters.PageSize); 
        var pagedList = new PagedList<DishesOrders>(items, pageParameters, await items.CountAsync());
        return pagedList;
    }

    public async Task SaveAsync()
    {
        await this._db.SaveChangesAsync();
    }
}