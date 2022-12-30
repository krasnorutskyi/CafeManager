using System.Linq.Expressions;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;

namespace CafeManager.Application.IRepositories;

public interface IGenericRepository<TEntity> where TEntity : EntityBase
{
    Task AddAsync(TEntity entity);

    Task UpdateAsync(TEntity entity);

    Task DeleteAsync(TEntity entity);

    void Attach(params object[] obj);

    Task<TEntity> GetOneAsync(int id);

    Task<TEntity> GetOneAsync(int id, params Expression<Func<TEntity, object>>[] includeProperties);
    
    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties);

    Task<PagedList<TEntity>> GetPageAsync(PageParameters pageParameters);

    Task<PagedList<TEntity>> GetPageAsync(PageParameters pageParameters,
        params Expression<Func<TEntity, object>>[] includeProperties);

    Task<PagedList<TEntity>> GetPageAsync(PageParameters pageParameters,
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties);

    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);

    Task SaveAsync();
}