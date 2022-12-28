namespace CafeManager.Application.Paging;

public class PagedList<T> : List<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
    
    public int TotalItems { get; set; }

    public PagedList() { }

    public PagedList(IEnumerable<T> items, PageParameters pageParameters, int totalItems)
    {
        this.PageNumber = pageParameters.PageNumber;
        this.PageSize = pageParameters.PageSize;
        this.TotalItems = totalItems;
        this.TotalPages = (int)Math.Ceiling((double)totalItems / pageParameters.PageSize);

        this.AddRange(items);
    }

}