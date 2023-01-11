using CafeManager.Application.Paging;
using CafeManager.Core.Entities;
using CafeManager.Core.StatisticsModels;

namespace CafeManager.Application.IRepositories;

public interface IStatisticsRepository
{
    Task<PagedList<Dish>> GetDishesStatisticsAsync(PageParameters pageParameters);

    Task<DateTime> GetBusiestDayStatisticsAsync();

    Task<PagedList<WaiterStatisticsModel>> GetWaitersStatisticsAsync(PageParameters pageParameters);

    Task<PagedList<TableStatisticsModel>> GetTablesStatisticsAsync(PageParameters pageParameters);
}