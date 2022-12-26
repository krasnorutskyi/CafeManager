using System.Linq.Expressions;
using CafeManager.Application.IRepositories;
using CafeManager.Application.IServices;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;

namespace CafeManager.Infrastructure.Services;

public class CategoryService : ICategoryService
{
    private readonly IGenericRepository<Category> _categoryRepository;

    public CategoryService(IGenericRepository<Category> categoryRepository)
    {
        this._categoryRepository = categoryRepository;
    }

    public async Task AddAsync(Category category)
    {
        this._categoryRepository.Attach(category);
        await this._categoryRepository.AddAsync(category);
    }

    public async Task UpdateAsync(Category category)
    {
        this._categoryRepository.Attach(category);
        await this._categoryRepository.UpdateAsync(category);
    }

    public async Task DeleteAsync(Category category)
    {
        await this._categoryRepository.DeleteAsync(category);
    }

    public async Task<Category> GetOneAsync(int id)
    {
        return await this._categoryRepository.GetOneAsync(id);
    }

    public async Task<Category> GetOneAsync(int id, params Expression<Func<Category, object>>[] includeProperties)
    {
        return await this._categoryRepository.GetOneAsync(id, includeProperties);
    }

    public async Task<PagedList<Category>> GetPageAsync(PageParameters pageParameters)
    {
        return await this._categoryRepository.GetPageAsync(pageParameters);
    }

    public async Task<PagedList<Category>> GetPageAsync(PageParameters pageParameters, params Expression<Func<Category, object>>[] includeProperties)
    {
        return await this._categoryRepository.GetPageAsync(pageParameters, includeProperties);
    }

    public async Task<PagedList<Category>> GetPageAsync(PageParameters pageParameters, Expression<Func<Category, bool>> predicate, params Expression<Func<Category, object>>[] includeProperties)
    {
        return await this._categoryRepository.GetPageAsync(pageParameters, predicate, includeProperties);
    }
}