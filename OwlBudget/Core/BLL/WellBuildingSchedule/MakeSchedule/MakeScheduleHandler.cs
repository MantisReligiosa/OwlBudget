using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.BLL.Scenario.GetProjectScenarios;
using Core.DAL.RepositoryInterfaces;
using Core.Domain;
using MediatR;

namespace Core.BLL.WellBuildingSchedule.MakeSchedule;

public class MakeScheduleHandler : IRequestHandler<MakeScheduleRequest, List<Dictionary<string, object>>>
{
    private readonly IClusterRepository _clusterRepository;
    private readonly IMediator _mediator;
    private readonly IWellBuildingScheduleRepository _wellBuildingScheduleRepository;
    private readonly IWellRepository _wellRepository;

    public MakeScheduleHandler(IMediator mediator, IWellBuildingScheduleRepository wellBuildingScheduleRepository,
        IClusterRepository clusterRepository, IWellRepository wellRepository)
    {
        _mediator = mediator;
        _wellBuildingScheduleRepository = wellBuildingScheduleRepository;
        _clusterRepository = clusterRepository;
        _wellRepository = wellRepository;
    }

    public async Task<List<Dictionary<string, object>>> Handle(MakeScheduleRequest request,
        CancellationToken cancellationToken)
    {
        var scenarios = await _mediator.Send(new GetProjectScenariosRequest(request.ProjectId),
            cancellationToken);
        var schedules = await _wellBuildingScheduleRepository.GetByProjectIdAsync(request.ProjectId, cancellationToken);

        var constructionScheduleTemplateTypes = ConstructionScheduleTemplates.All;

        var result = new List<Dictionary<string, object>>();
        foreach (var scenarioId in scenarios.Select(_ => _.Id))
        foreach (var templateType in constructionScheduleTemplateTypes)
        {
            var item = new Dictionary<string, object>
            {
                {"caption", templateType.Description},
                {"scenarioId", scenarioId},
                {"templateType", (int) templateType.TemplateType},
                {"isDateEndEnabled", ConstructionScheduleTemplates.HaveVariableLatency(templateType.TemplateType)}
            };
            foreach (var schedule in schedules.Where(_ =>
                         _.ScenarioId == scenarioId && _.TemplateType == templateType.TemplateType))
            {
                item.Add(schedule.Well.Id.ToString(), new
                {
                    id = schedule.Id,
                    wellId = schedule.Well.Id,
                    duration = schedule.DurationDays
                });
            }

            result.Add(item);
        }

        return result;
    }
}