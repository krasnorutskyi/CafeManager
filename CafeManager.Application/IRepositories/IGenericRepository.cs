using CafeManager.Core.Entities;

namespace CafeManager.Application.IRepositories;

public interface IGenericRepository<TEntity> where TEntity : EntityBase
{
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task UpdateRangeAsync(IEnumerable<TEntity> entities);
    Task<TEntity> GetOneAsync(int id);
}