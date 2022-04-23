using System;
using MediatR;

namespace Core.BLL.Scenario.CreateScenario;

public record CreateScenarioRequest(Guid ProjectId, int BudgetType) : IRequest;