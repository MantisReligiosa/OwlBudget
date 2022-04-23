using System.Collections.Generic;

namespace OwlBudget.Models;

public class TableResponse<T>
    where T : class
{
    public List<T> Rows { get; set; }
    public int TotalRows { get; set; }
    public int Page { get; set; }
    public int PerPage { get; set; }
}