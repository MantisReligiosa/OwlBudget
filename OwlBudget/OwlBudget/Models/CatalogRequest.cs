namespace OwlBudget.Models;

public class CatalogRequest
{
    public int CurrentPage { get; set; }
    public int PerPage { get; set; }
    public string Filter { get; set; }
    public string SortBy { get; set; }
    public bool SortDesc { get; set; }
}