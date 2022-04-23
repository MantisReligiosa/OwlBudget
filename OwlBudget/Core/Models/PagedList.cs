using System.Collections.Generic;
using Core.Domain;

namespace Core.Models;

public class PagedList<TNamedIdentity>
    where TNamedIdentity : NamedIdentity
{
    public List<TNamedIdentity> Items { get; set; }

    public int PerPage { get; set; }

    public int Page { get; set; }

    public int TotalRows { get; set; }
}