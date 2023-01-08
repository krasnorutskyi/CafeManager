using System.Linq.Expressions;
using CafeManager.Application.IRepositories;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;
using CafeManager.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace CafeManager.Infrastructure.Repositories;

public class DishRepository : IDishRepository
{
    private readonly ApplicationContext _db;
    private readonly DbSet<Dish> _table;

    public DishRepository(ApplicationContext context)
    {
        this._db = context;
        this._table = _db.Set<Dish>();
    }


    public async Task AddAsync(Dish entity)
    {
        await this._table.AddAsync(entity);
        await this.SaveAsync();
    }

    public async Task UpdateAsync(Dish entity)
    {
        this._table.Update(entity);
        await this.SaveAsync();
    }

    public async Task DeleteAsync(Dish entity)
    {
        this._table.Remove(entity);
        await this.SaveAsync();
    }

    public void Attach(params object[] obj)
    {
        this._db.AttachRange(obj);
    }

    public async Task<Dish> GetOneAsync(int id)
    {
        return await this._table.FirstOrDefaultAsync<Dish>(entity => entity.Id == id);
    }

    public async Task<Dish> GetDishWithRelatedAsync(int id)
    {
        var dish = this._table.Where(entity => entity.Id == id)
            .Include(dish=>dish.DishesProducts)
            .ThenInclude(dishesProducts => dishesProducts.Product);

        return await dish.FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<Dish> GetOneAsync(int id, params Expression<Func<Dish, object>>[] includeProperties)
    {
        var query = this._table.Where(entity => entity.Id == id);
        foreach (var property in includeProperties)
        {
            query = query.Include(property);
        }

        return await query.FirstOrDefaultAsync<Dish>(entity => entity.Id == id);
    }
    
    public async Task<IEnumerable<Dish>> GetAllAsync()
    {
        var query = this._table.AsNoTracking();
        return await query.ToListAsync<Dish>();
    }

    public async Task<IEnumerable<Dish>> GetAllAsync(Expression<Func<Dish, bool>> predicate,
        params Expression<Func<Dish, object>>[] includeProperties)
    {
        var query = this._table.Where(predicate);
        foreach (var property in includeProperties)
        {
            query = query.Include(property);
        }

        return await query.ToListAsync<Dish>();
    }

    public async Task<PagedList<Dish>> GetPageAsync(PageParameters pageParameters)
    {
        var items = await this._table.AsNoTracking()
            .Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
            .Take(pageParameters.PageSize)
            .ToListAsync();
        
        var itemsCount = this._table.Count();
        var pagedList = new PagedList<Dish>(items, pageParameters, itemsCount);
        
        return pagedList;
    }

    public async Task<PagedList<Dish>> GetPageAsync(PageParameters pageParameters,
        params Expression<Func<Dish, object>>[] includeProperties)
    {
        var items = this._table.AsNoTracking()
            .Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
            .Take(pageParameters.PageSize);
        foreach (var property in includeProperties)
        {
            items = items.Include(property);
        }
        var itemsCount = this._table.Count();
        var pagedList = new PagedList<Dish>(items, pageParameters, itemsCount);
        return pagedList;
    }

    public async Task<PagedList<Dish>> GetPageAsync(PageParameters pageParameters,
        Expression<Func<Dish, bool>> predicate,
        params Expression<Func<Dish, object>>[] includeProperties)
    {
        var items = this._table.AsNoTracking()
            .Where(predicate)
            .Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
            .Take(pageParameters.PageSize); ;
        foreach (var property in includeProperties)
        {
            items = items.Include(property);
        }
        var itemsCount = this._table.Count();
        var pagedList = new PagedList<Dish>(items, pageParameters, itemsCount);
        return pagedList;
    }

    public async Task<bool> ExistsAsync(Expression<Func<Dish, bool>> predicate)
    {
        return await this._table.AnyAsync<Dish>(predicate);
    }

    public async Task SaveAsync()
    {
        await this._db.SaveChangesAsync();
    }
}