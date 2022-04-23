using System;
using System.Collections.Generic;
using MediatR;

namespace Core.BLL.Scenario.GetProjectScenarios;

public record GetProjectScenariosRequest(Guid ProjectId) : IRequest<List<Domain.Scenario>>;