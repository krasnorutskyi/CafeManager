using System.Linq.Expressions;
using CafeManager.Application.IRepositories;
using CafeManager.Application.IServices;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Table = iText.Layout.Element.Table;

namespace CafeManager.Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly IGenericRepository<Order> _orderRepository;
    private readonly IGenericRepository<Dish> _dishRepository;

    public OrderService(IGenericRepository<Order> orderRepository, IGenericRepository<Dish> dishRepository)
    {
        this._orderRepository = orderRepository;
        this._dishRepository = dishRepository;
    }

    public async Task<byte[]> GenerateInvoice(int id)
    {
        var order = await this._orderRepository.GetOneAsync(id, o=>o.Waiter, o=> o.DishesOrders);
        
        //using var pdfReader = new PdfReader("");
        using var stream = new MemoryStream();
        using var writer = new PdfWriter(stream);
        using var pdf = new PdfDocument(writer);
        Document document = new Document(pdf);

        Paragraph header = new Paragraph($"Order №{order.Id}\n {order.Date.ToShortDateString()}")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(20);

        document.Add(header);

        Paragraph body = new Paragraph($"Waiter: {order.Waiter.FirstName} {order.Waiter.LastName}\n Table: {order.TableId}")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(14);
        
        document.Add(body);

        foreach (var d in order.DishesOrders)
        {
            Paragraph dish = new Paragraph($"{d.DishName} ............... {d.DishesAmount} * {(d.DishesTotal/d.DishesAmount).ToString()} .......... {d.DishesTotal}")
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetFontSize(14);

            document.Add(dish);
        }

        document.Add(new LineSeparator(new DottedLine()));

        Paragraph footter = new Paragraph($"Total - {order.Price}\n Value Added Tax - {order.VAT}")
            .SetTextAlignment(TextAlignment.RIGHT)
            .SetFontSize(16);
        
        document.Add(footter);
        document.Close();
        pdf.Close();
        return stream.ToArray();
    }

    public async Task<byte[]> GenerateSalesReport(DateTime date)
    {
        var orders = await this._orderRepository.GetAllAsync(o=>o.DishesOrders);
        var order = orders.Where(o => o.Date.ToShortDateString() == date.ToShortDateString()).ToList();
        var dishesOrders = new List<DishesOrders>();
        foreach (var o in order)
        {
            foreach (var d in o.DishesOrders)
            {
                dishesOrders.Add(d);
            }
        }

        var dishes = await this._dishRepository.GetAllAsync();

        using var stream = new MemoryStream();
        using var writer = new PdfWriter(stream);
        using var pdf = new PdfDocument(writer);
        Document document = new Document(pdf);
        
        Paragraph header = new Paragraph($"Sales Report :{date.ToShortDateString()}")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(20);

        document.Add(header);
        
        document.Add(new LineSeparator(new DottedLine()));

        Table table = new Table(4, true);
        
        Cell cell11 = new Cell(1, 1)
            .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
            .SetTextAlignment(TextAlignment.CENTER)
            .Add(new Paragraph("Dish"));
        
        Cell cell12 = new Cell(1, 1)
            .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
            .SetTextAlignment(TextAlignment.CENTER)
            .Add(new Paragraph("Sales"));
        
        Cell cell13 = new Cell(1, 1)
            .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
            .SetTextAlignment(TextAlignment.CENTER)
            .Add(new Paragraph("Price per unit"));
        
        Cell cell14 = new Cell(1, 1)
            .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
            .SetTextAlignment(TextAlignment.CENTER)
            .Add(new Paragraph("Income"));

        table.AddCell(cell11);
        table.AddCell(cell12);
        table.AddCell(cell13);
        table.AddCell(cell14);

        float total = 0;
        foreach (var d in dishes)
        {
            var cell1 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph($"{d.Name}"));
            
            var sales = dishesOrders.Where(dishesOrder => dishesOrder.DishId == d.Id).ToList();
            int salesProUnit = 0;
            foreach (var s in sales)
            {
                salesProUnit += s.DishesAmount;
            }
            
            var cell2 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph($"{salesProUnit}"));
            
            var cell3 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph($"{d.Price}"));
            
            var cell4 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph($"{d.Price * salesProUnit}"));

            total += (d.Price * salesProUnit);
            
            table.AddCell(cell1);
            table.AddCell(cell2);
            table.AddCell(cell3);
            table.AddCell(cell4);
        }

        document.Add(table);
        
        document.Add(new LineSeparator(new SolidLine()));
        
        Paragraph footer = new Paragraph($"Total Income :{total}")
            .SetTextAlignment(TextAlignment.RIGHT)
            .SetFontSize(20);

        document.Add(footer);
        
        document.Close();
        pdf.Close();
        return stream.ToArray();
    }

    public async Task AddAsync(Order order)
    {
        this._orderRepository.Attach(order);
        await this._orderRepository.AddAsync(order);
    }

    public async Task UpdateAsync(Order order)
    {
        this._orderRepository.Attach(order);
        await this._orderRepository.UpdateAsync(order);
    }

    public async Task DeleteAsync(Order order)
    {
        await this._orderRepository.DeleteAsync(order);
    }

    public async Task<Order> GetOneAsync(int id)
    {
        return await this._orderRepository.GetOneAsync(id);
    }

    public async Task<Order> GetOneAsync(int id, params Expression<Func<Order, object>>[] includeProperties)
    {
        return await this._orderRepository.GetOneAsync(id, includeProperties);
    }

    public async Task<PagedList<Order>> GetPageAsync(PageParameters pageParameters)
    {
        return await this._orderRepository.GetPageAsync(pageParameters);
    }

    public async Task<PagedList<Order>> GetPageAsync(PageParameters pageParameters, params Expression<Func<Order, object>>[] includeProperties)
    {
        return await this._orderRepository.GetPageAsync(pageParameters, includeProperties);
    }

    public async Task<PagedList<Order>> GetPageAsync(PageParameters pageParameters, Expression<Func<Order, bool>> predicate, params Expression<Func<Order, object>>[] includeProperties)
    {
        return await this._orderRepository.GetPageAsync(pageParameters, predicate, includeProperties);
    }
}