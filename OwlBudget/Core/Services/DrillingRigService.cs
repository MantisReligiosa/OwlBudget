using Core.DAL.RepositoryInterfaces;
using Core.Domain;
using Core.ServiceInterfaces;

namespace Core.Services;

public class DrillingRigService : BaseCatalogService<DrillingRig>, IDrillingRigService
{
    public DrillingRigService(IDrillingRigRepository drillingRigRepository)
        : base(drillingRigRepository)
    {
    }
}