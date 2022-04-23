using System;

namespace OwlBudget.Models.ProjectObjectsModels;

public class Scenario
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int BudgetType { get; set; }
}