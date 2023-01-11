using CafeManager.Application.Paging;
using CafeManager.Core.Entities;
using CafeManager.Core.StatisticsModels;

namespace CafeManager.Application.IServices;

public interface IStatisticsService
{
    Task<PagedList<Dish>> GetDishesStatisticsAsync(PageParameters pageParameters);

    Task<DateTime> GetLeastBusyDayStatisticsAsync();

    Task<PagedList<WaiterStatisticsModel>> GetWaitersStatisticsAsync(PageParameters pageParameters);

    Task<PagedList<TableStatisticsModel>> GetTablesStatisticsAsync(PageParameters pageParameters);
}