using System.Linq.Expressions;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;

namespace CafeManager.Application.IServices;

public interface ITableService
{
    Task AddAsync(Table entity);
    Task UpdateAsync(Table entity);
    Task DeleteAsync(Table entity);
    Task<Table> GetOneAsync(int id);
    Task<PagedList<Table>> GetPageAsync(PageParameters pageParameters);
    Task<PagedList<Table>> GetPageAsync(PageParameters pageParameters, Expression<Func<Table, bool>> predicate);
}