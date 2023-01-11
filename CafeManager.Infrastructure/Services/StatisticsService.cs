using CafeManager.Application.IRepositories;
using CafeManager.Application.IServices;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;
using CafeManager.Core.StatisticsModels;

namespace CafeManager.Infrastructure.Services;

public class StatisticsService : IStatisticsService
{
    private readonly IStatisticsRepository _statisticsRepository;
    private readonly IUnitService _unitService;
    private readonly ICategoryService _categoryService;

    public StatisticsService(IStatisticsRepository statisticsRepository, IUnitService unitService, ICategoryService categoryService)
    {
        this._statisticsRepository = statisticsRepository;
        this._unitService = unitService;
        this._categoryService = categoryService;
    }
    
    public async Task<PagedList<Dish>> GetDishesStatisticsAsync(PageParameters pageParameters)
    {
        pageParameters.PageSize = 1;
        var dishes =  await this._statisticsRepository.GetDishesStatisticsAsync(pageParameters);
        foreach (var d in dishes)
        {
            d.Category = await this._categoryService.GetOneAsync(d.CategoryId);
            d.Unit = await this._unitService.GetOneAsync(d.UnitId);
        }

        return dishes;
    }

    public async Task<DateTime> GetLeastBusyDayStatisticsAsync()
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