using System;

namespace OwlBudget.Models.TableResponse;

public class ProjectCatalogItem : CatalogItem
{
    public string Description { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public int CreationYear => Created.Year;
}