using CafeManager.Application.Paging;
using CafeManager.Core.Entities;

namespace CafeManager.Application.IRepositories;

public interface IProductRepository
{
    Task<PagedList<Product>> GetSortedPageAsync(PageParameters pageParameters);
}