namespace Core.Models;

public class PagingContext
{
    public int UserId { get; set; }

    public string Filter { get; set; }

    public string SortBy { get; set; }

    public int CurrentPage { get; set; }

    public int PerPage { get; set; }

    public bool SortDesc { get; set; }
}