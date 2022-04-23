using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain;

namespace Core.DAL.RepositoryInterfaces;

public interface IWellBuildingScheduleRepository : IRepository<WellBuildingScheduleItem>
{
    Task<List<WellBuildingScheduleItem>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken);
    Task<List<WellBuildingScheduleItem>> GetByWellIdAsync(Guid wellId, CancellationToken cancellationToken);
}