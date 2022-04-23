using System.Linq;
using AutoMapper;
using OwlBudget.Models.ProjectObjectsModels;
using Well = Core.Domain.Well;

namespace OwlBudget.Profiles;

public class WellProfile : Profile
{
    public WellProfile()
    {
        CreateMap<Well, Models.ProjectObjectsModels.Well>()
            .ForMember(_ => _.DrillingRigsToWell, _ => _.Ignore())
            .AfterMap((domain, model) =>
            {
                model.DrillingRigsToWell = domain.WellsToDrillingRigSequences.Select(_ => new DrillingRigToWell
                {
                    DrillingRigId = _.DrillingRigSequence.DrillingRigId,
                    DrillingRigName = _.DrillingRigSequence.DrillingRig.Name,
                    ScenarioId = _.DrillingRigSequence.ScenarioId
                }).ToList();
            });
    }
}