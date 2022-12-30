using System.Linq.Expressions;
using CafeManager.Application.IRepositories;
using CafeManager.Application.IServices;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;

namespace CafeManager.Infrastructure.Services;

public class UnitService : IUnitService
{
    private readonly IGenericRepository<Unit> _unitRepository;

    public UnitService(IGenericRepository<Unit> unitRepository)
    {
        this._unitRepository = unitRepository;
    }
    
    public async Task AddAsync(Unit unit)
    {
        this._unitRepository.Attach(unit);
        await this._unitRepository.AddAsync(unit);
    }

    public async Task UpdateAsync(Unit unit)
    {
        this._unitRepository.Attach(unit);
        await this._unitRepository.UpdateAsync(unit);
    }

    public async Task DeleteAsync(Unit unit)
    {
        await this._unitRepository.DeleteAsync(unit);
    }

    public async Task<IEnumerable<Unit>> GetAllAsync()
    {
        return await this._unitRepository.GetAllAsync();
    }

    public async Task<Unit> GetOneAsync(int id)
    {
        return await this._unitRepository.GetOneAsync(id);
    }

    public async Task<Unit> GetOneAsync(int id, params Expression<Func<Unit, object>>[] includeProperties)
    {
        return await this._unitRepository.GetOneAsync(id, includeProperties);
    }

    public async Task<PagedList<Unit>> GetPageAsync(PageParameters pageParameters)
    {
        return await this._unitRepository.GetPageAsync(pageParameters);
    }

    public async Task<PagedList<Unit>> GetPageAsync(PageParameters pageParameters, params Expression<Func<Unit, object>>[] includeProperties)
    {
        return await this._unitRepository.GetPageAsync(pageParameters, includeProperties);
    }

    public async Task<PagedList<Unit>> GetPageAsync(PageParameters pageParameters, Expression<Func<Unit, bool>> predicate, params Expression<Func<Unit, object>>[] includeProperties)
    {
        return await this._unitRepository.GetPageAsync(pageParameters, predicate, includeProperties);
    }
}