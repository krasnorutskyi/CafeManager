using System.Data;
using System.Text;
using CafeManager.Application.IRepositories;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;
using CafeManager.Core.StatisticsModels;
using CafeManager.Infrastructure.EF;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CafeManager.Infrastructure.Repositories;

public class StatisticsRepository : IStatisticsRepository
{
    private readonly ApplicationContext _db;

    public StatisticsRepository(ApplicationContext db)
    {
        this._db = db;
    }
    
    
    public async Task<PagedList<Dish>> GetDishesStatisticsAsync(PageParameters pageParameters)
    {
        var json = new StringBuilder();
        using (var command = this._db.Database.GetDbConnection().CreateCommand())
        {
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Dishes" +
                                  " ORDER BY Sales DESC" +
                                  "OFFSET @offset ROWS" +
                                  "FETCH NEXT @pageSize ROWS ONLY" +
                                  "FOR JSON PATH";
            
            command.Parameters.Add(new SqlParameter("@offset", (pageParameters.PageNumber - 1) * pageParameters.PageSize));
            command.Parameters.Add(new SqlParameter("@pageSize", pageParameters.PageSize));
            
            await this._db.Database.OpenConnectionAsync();
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    json.Append(reader.GetValue(0).ToString());
                }
            }
        }
        var dishes = JsonConvert.DeserializeObject<List<Dish>>(json.ToString());
        var count = await this._db.Dishes.CountAsync();

        return new PagedList<Dish>(dishes, pageParameters, count);
    }

    public async Task<DateTime> GetBusiestDayStatisticsAsync()
    {
        var json = new StringBuilder();
        using (var command = this._db.Database.GetDbConnection().CreateCommand())
        {
            command.CommandType = CommandType.Text;
            command.CommandText = "Select Distinct Date" +
                                  " FROM Orders" +
                                  " Group By Date" +
                                  " Order BY Count(Date) Desc" +
                                  " OFFSET 0 rows " +
                                  "Fetch next 1 rows only;";
            
            
            await this._db.Database.OpenConnectionAsync();
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    json.Append(reader.GetValue(0).ToString());
                }
            }
        }

        var dateTime = JsonConvert.DeserializeObject<DateTime>(json.ToString());

        return dateTime;
    }

    public async Task<PagedList<WaiterStatisticsModel>> GetWaitersStatisticsAsync(PageParameters pageParameters)
    {
        var json = new StringBuilder();
        using (var command = this._db.Database.GetDbConnection().CreateCommand())
        {
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT distinct" +
                                  " FirstName, LastName, Count(Orders.Id)" +
                                  " FROM Order " +
                                  "JOIN Waiters ON Waiters.Id=Orders.WaiterId " +
                                  "Group by FirstName, LastName";
            
            
            await this._db.Database.OpenConnectionAsync();
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    json.Append(reader.GetValue(0).ToString());
                }
            }
        }
        
        var waiters = JsonConvert.DeserializeObject<List<WaiterStatisticsModel>>(json.ToString());
        var count = waiters.Count;
        
        return new PagedList<WaiterStatisticsModel>(waiters, pageParameters, count);
    }

    public async Task<PagedList<TableStatisticsModel>> GetTablesStatisticsAsync(PageParameters pageParameters)
    {
        var json = new StringBuilder();
        using (var command = this._db.Database.GetDbConnection().CreateCommand())
        {
            command.CommandType = CommandType.Text;
            command.CommandText = "Select Distinct " +
                                  "TableId, Count(Id)" +
                                  " from Orders " +
                                  "GROUP by TableId" +
                                  " having COUNT(id) = " +
                                  "(Select Count(id) " +
                                  "From Orders" +
                                  " GROUP by TableId" +
                                  " order BY Count(id)Desc" +
                                  " OFFSET 0 rows " +
                                  "fetch next 1 rows only)";


            await this._db.Database.OpenConnectionAsync();
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    json.Append(reader.GetValue(0).ToString());
                }
            }
        }
        var tables = JsonConvert.DeserializeObject<List<TableStatisticsModel>>(json.ToString());
        var count = tables.Count;
        
        return new PagedList<TableStatisticsModel>(tables, pageParameters, count);
    }
}