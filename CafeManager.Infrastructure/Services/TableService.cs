using System.Linq.Expressions;
using CafeManager.Application.IRepositories;
using CafeManager.Application.IServices;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;

namespace CafeManager.Infrastructure.Services;

public class TableService : ITableService
{
    private readonly IGenericRepository<Table> _tableRepository;

    public TableService(IGenericRepository<Table> tableRepository)
    {
        this._tableRepository = tableRepository;
    }

    public async Task AddAsync(Table table)
    {
        this._tableRepository.Attach(table);
        await this._tableRepository.AddAsync(table);
    }

    public async Task UpdateAsync(Table table)
    {
        this._tableRepository.Attach(table);
        await this._tableRepository.UpdateAsync(table);
    }

    public async Task DeleteAsync(Table table)
    {
        await this._tableRepository.DeleteAsync(table);
    }

    public async Task<Table> GetOneAsync(int id)
    {
        return await this._tableRepository.GetOneAsync(id);
    }

    public async Task<Table> GetOneAsync(int id, params Expression<Func<Table, object>>[] includeProperties)
    {
        return await this._tableRepository.GetOneAsync(id, includeProperties);
    }

    public async Task<PagedList<Table>> GetPageAsync(PageParameters pageParameters)
    {
        return await this._tableRepository.GetPageAsync(pageParameters);
    }

    public async Task<PagedList<Table>> GetPageAsync(PageParameters pageParameters, params Expression<Func<Table, object>>[] includeProperties)
    {
        return await this._tableRepository.GetPageAsync(pageParameters, includeProperties);
    }

    public async Task<PagedList<Table>> GetPageAsync(PageParameters pageParameters, Expression<Func<Table, bool>> predicate, params Expression<Func<Table, object>>[] includeProperties)
    {
        return await this._tableRepository.GetPageAsync(pageParameters, predicate, includeProperties);
    }
}