using AutoMapper;
using Core.DAL.Entities;
using Core.Domain;

namespace Core.Profiles;

public class LotProfile : Profile
{
    public LotProfile()
    {
        CreateMap<LotEntity, Lot>();
        CreateMap<Lot, LotEntity>()
            .ForMember(_ => _.Project, _ => _.Ignore());
    }
}