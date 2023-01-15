using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using CafeManager.Application.IRepositories;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;
using CafeManager.Infrastructure.EF;

namespace CafeManager.Infrastructure.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : EntityBase
{
    private readonly ApplicationContext _db;
    private readonly DbSet<TEntity> _table;

    public GenericRepository(ApplicationContext context)
    {
        this._db = context;
        this._table = _db.Set<TEntity>();
    }
    

    public async Task AddAsync(TEntity entity)
    {
        await this._table.AddAsync(entity);
        await this.SaveAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        this._table.Update(entity);
        await this.SaveAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        this._table.Remove(entity);
        await this.SaveAsync();
    }

    public void Attach(params object[] obj)
    {
        this._db.AttachRange(obj);
    }

    public async Task<TEntity> GetOneAsync(int id)
    {
        return await this._table.FirstOrDefaultAsync<TEntity>(entity => entity.Id == id);
    }

    public async Task<TEntity> GetOneAsync(int id, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var query = this._table.Where(entity => entity.Id == id);
        foreach (var property in includeProperties)
        {
            query = query.Include(property);
        }

        return await query.FirstOrDefaultAsync<TEntity>(entity => entity.Id == id);
    }
    
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var query = this._table.AsNoTracking();
        return await query.ToListAsync<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var query = this._table.Where(predicate);
        foreach (var property in includeProperties)
        {
            query = query.Include(property);
        }

        return await query.ToListAsync<TEntity>();
    }
    
    public async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var query = this._table.AsNoTracking();
        foreach (var property in includeProperties)
        {
            query = query.Include(property);
        }

        return await query.ToListAsync<TEntity>();
    }

    public async Task<PagedList<TEntity>> GetPageAsync(PageParameters pageParameters)
    {
        var items = await this._table.AsNoTracking()
            .Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
            .Take(pageParameters.PageSize)
            .ToListAsync();
        
        var itemsCount = this._table.Count();
        var pagedList = new PagedList<TEntity>(items, pageParameters, itemsCount);
        
        return pagedList;
    }

    public async Task<PagedList<TEntity>> GetPageAsync(PageParameters pageParameters,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var items = this._table.AsNoTracking()
            .Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
            .Take(pageParameters.PageSize);
        foreach (var property in includeProperties)
        {
            items = items.Include(property);
        }
        var itemsCount = this._table.Count();
        var pagedList = new PagedList<TEntity>(items, pageParameters, itemsCount);
        return pagedList;
    }

    public async Task<PagedList<TEntity>> GetPageAsync(PageParameters pageParameters,
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties)
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
        var pagedList = new PagedList<TEntity>(items, pageParameters, itemsCount);
        return pagedList;
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await this._table.AnyAsync<TEntity>(predicate);
    }

    public async Task SaveAsync()
    {
        await this._db.SaveChangesAsync();
    }
}