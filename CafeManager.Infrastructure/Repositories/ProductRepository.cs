using CafeManager.Application.IRepositories;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;
using CafeManager.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace CafeManager.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    
    private readonly ApplicationContext _db;
    private readonly DbSet<Product> _table;

    public ProductRepository(ApplicationContext context)
    {
        this._db = context;
        this._table = _db.Set<Product>();
    }
    
    public async Task<PagedList<Product>> GetSortedPageAsync(PageParameters pageParameters)
    {
        var items = new List<Product>();
        if (pageParameters.Sort == "Newest")
        {
            items = await this._table.AsNoTracking().OrderByDescending(p=>p.BestBefore)
                .Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
                .Take(pageParameters.PageSize)
                .ToListAsync();
        }
        else
        {
            items = await this._table.AsNoTracking().OrderBy(p=>p.BestBefore)
                .Skip((pageParameters.PageNumber - 1) * pageParameters.PageSize)
                .Take(pageParameters.PageSize)
                .ToListAsync();
        }
        
        
        
        var itemsCount = this._table.Count();
        var pagedList = new PagedList<Product>(items, pageParameters, itemsCount);
        
        return pagedList;
    }
}