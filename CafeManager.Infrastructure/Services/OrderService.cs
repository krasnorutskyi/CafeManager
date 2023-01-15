using System.Linq.Expressions;
using CafeManager.Application.IRepositories;
using CafeManager.Application.IServices;
using CafeManager.Application.Paging;
using CafeManager.Core.Entities;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace CafeManager.Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly IGenericRepository<Order> _orderRepository;

    public OrderService(IGenericRepository<Order> orderRepository)
    {
        this._orderRepository = orderRepository;
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