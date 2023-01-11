using CafeManager.Application.IRepositories;
using CafeManager.Application.IServices;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;
using CafeManager.Core.StatisticsModels;

namespace CafeManager.Infrastructure.Services;

public class StatisticsService : IStatisticsService
{
    private readonly IStatisticsRepository _statisticsRepository;

    public StatisticsService(IStatisticsRepository statisticsRepository)
    {
        this._statisticsRepository = statisticsRepository;
    }
    
    public async Task<PagedList<Dish>> GetDishesStatisticsAsync(PageParameters pageParameters)
    {
        pageParameters.PageSize = 1;
        return await this._statisticsRepository.GetDishesStatisticsAsync(pageParameters);
    }

    public async Task<DateTime> GetBusiestDayStatisticsAsync()
    {
        return await this._statisticsRepository.GetBusiestDayStatisticsAsync();
    }

    public async Task<PagedList<WaiterStatisticsModel>> GetWaitersStatisticsAsync(PageParameters pageParameters)
    {
        return await this._statisticsRepository.GetWaitersStatisticsAsync(pageParameters);
    }

    public async Task<PagedList<TableStatisticsModel>> GetTablesStatisticsAsync(PageParameters pageParameters)
    {
        return await this._statisticsRepository.GetTablesStatisticsAsync(pageParameters);
    }
}