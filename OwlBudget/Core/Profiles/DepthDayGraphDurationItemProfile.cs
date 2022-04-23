using AutoMapper;
using Core.DAL.Entities;
using Core.Domain;

namespace Core.Profiles;

public class DepthDayGraphDurationItemProfile : Profile
{
    public DepthDayGraphDurationItemProfile()
    {
        CreateMap<DepthDayGraphDurationItemEntity, DepthDayGraphDurationItem>();

        CreateMap<DepthDayGraphDurationItem, DepthDayGraphDurationItemEntity>();
    }
}