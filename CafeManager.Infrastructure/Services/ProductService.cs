using System.Linq.Expressions;
using CafeManager.Application.IRepositories;
using CafeManager.Application.IServices;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;
using Microsoft.IdentityModel.Tokens;

namespace CafeManager.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly IGenericRepository<Product> _productRepository;
    private readonly IProductRepository _repository;

    public ProductService(IGenericRepository<Product> productRepository, IProductRepository repository)
    {
        this._productRepository = productRepository;
        this._repository = repository;
    }

    public async Task AddAsync(Product product)
    {
        this._productRepository.Attach(product);
        await this._productRepository.AddAsync(product);
    }

    public async Task UpdateAsync(Product product)
    {
        this._productRepository.Attach(product);
        await this._productRepository.UpdateAsync(product);
    }

    public async Task DeleteAsync(Product product)
    {
        await this._productRepository.DeleteAsync(product);
    }

    public async Task<Product> GetOneAsync(int id)
    {
        return await this._productRepository.GetOneAsync(id);
    }

    public async Task<Product> GetOneAsync(int id, params Expression<Func<Product, object>>[] includeProperties)
    {
        return await this._productRepository.GetOneAsync(id, includeProperties);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await this._productRepository.GetAllAsync();
    }
    
    public async Task<PagedList<Product>> GetPageAsync(PageParameters pageParameters)
    {
        if (!pageParameters.Sort.IsNullOrEmpty())
        {
            return await this._repository.GetSortedPageAsync(pageParameters);
        }

        return await this._productRepository.GetPageAsync(pageParameters);
    }

    public async Task<PagedList<Product>> GetPageAsync(PageParameters pageParameters, params Expression<Func<Product, object>>[] includeProperties)
    {
        return await this._productRepository.GetPageAsync(pageParameters, includeProperties);
    }

    public async Task<PagedList<Product>> GetPageAsync(PageParameters pageParameters, Expression<Func<Product, bool>> predicate, params Expression<Func<Product, object>>[] includeProperties)
    {
        return await this._productRepository.GetPageAsync(pageParameters, predicate, includeProperties);
    }
}