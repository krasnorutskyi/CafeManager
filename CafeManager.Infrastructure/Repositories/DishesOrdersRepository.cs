using System.Linq.Expressions;
using CafeManager.Application.IRepositories;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CafeManager.Infrastructure.Repositories;

public class DishesOrdersRepository<T> : IDishesOrdersRepository<T> where T : DishesOrders
{
    private readonly DbContext _db;
    private readonly DbSet<T> _table;

    public DishesOrdersRepository(DbContext context)
    {
        _db = context;
        this._table = _db.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        await this._table.AddAsync(entity);
        await this.SaveAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        this._table.Update(entity);
        await this.SaveAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        this._table.Remove(entity);
        await this.SaveAsync();
    }

    public void Attach(params object[] obj)
    {
        this._db.AttachRange(obj);
    }

    public async Task<T> GetOneAsync(int dishid, int ordersNumber)
    {
        return await this._table.FirstOrDefaultAsync<T>(entity => entity.DishId == dishid && entity.OrdersNumber == ordersNumber );
    }

    public async Task<PagedList<T>> GetPageAsync(PageParameters pageParameters)
    {
        var items = this._table.AsNoTracking()
            .Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
            .Take(pageParameters.PageSize);
        var pagedList = new PagedList<T>(items, pageParameters, await items.CountAsync());
        return pagedList;
    }

    public async Task<PagedList<T>> GetPageAsync(PageParameters pageParameters, Expression<Func<T, bool>> predicate)
    {
        var items = this._table.AsNoTracking()
            .Where(predicate)
            .Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
            .Take(pageParameters.PageSize); ;
        var pagedList = new PagedList<T>(items, pageParameters, await items.CountAsync());
        return pagedList;
    }

    public async Task SaveAsync()
    {
        await this._db.SaveChangesAsync();
    }
}