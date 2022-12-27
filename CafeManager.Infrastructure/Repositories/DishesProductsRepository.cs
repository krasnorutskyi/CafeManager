using System.Linq.Expressions;
using CafeManager.Application.IRepositories;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;
using CafeManager.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace CafeManager.Infrastructure.Repositories;

public class DishesProductsRepository : IDishesProductsRepository
{
    private readonly ApplicationContext _db;
    private readonly DbSet<DishesProducts> _table;

    public DishesProductsRepository(ApplicationContext context)
    {
        this._db = context;
        this._table = _db.Set<DishesProducts>();
    }

    public async Task AddAsync(DishesProducts entity)
    {
        await this._table.AddAsync(entity);
        await this.SaveAsync();
    }

    public async Task UpdateAsync(DishesProducts entity)
    {
        this._table.Update(entity);
        await this.SaveAsync();
    }

    public async Task DeleteAsync(DishesProducts entity)
    {
        this._table.Remove(entity);
        await this.SaveAsync();
    }

    public void Attach(params object[] obj)
    {
        this._db.AttachRange(obj);
    }

    public async Task<DishesProducts> GetOneAsync(int dishId, int productId)
    {
        return await this._table.FirstOrDefaultAsync<DishesProducts>(entity => entity.DishId == dishId && entity.ProductId == productId );
    }

    public async Task<PagedList<DishesProducts>> GetPageAsync(PageParameters pageParameters)
    {
        var items = this._table.AsNoTracking()
            .Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
            .Take(pageParameters.PageSize);
        var pagedList = new PagedList<DishesProducts>(items, pageParameters, await items.CountAsync());
        return pagedList;
    }

    public async Task<PagedList<DishesProducts>> GetPageAsync(PageParameters pageParameters, Expression<Func<DishesProducts, bool>> predicate)
    {
        var items = this._table.AsNoTracking()
            .Where(predicate)
            .Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
            .Take(pageParameters.PageSize);
        var pagedList = new PagedList<DishesProducts>(items, pageParameters, await items.CountAsync());
        return pagedList;
    }

    public async Task SaveAsync()
    {
        await this._db.SaveChangesAsync();
    }

}