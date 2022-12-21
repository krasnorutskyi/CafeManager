using System.Linq.Expressions;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;

namespace CafeManager.Application.IServices;

public interface IUnitService
{
    Task AddAsync(Unit entity);
    Task UpdateAsync(Unit entity);
    Task DeleteAsync(Unit entity);
    Task<Unit> GetOneAsync(int id);
    Task<PagedList<Unit>> GetPageAsync(PageParameters pageParameters);
    Task<PagedList<Unit>> GetPageAsync(PageParameters pageParameters, Expression<Func<Unit, bool>> predicate);
}