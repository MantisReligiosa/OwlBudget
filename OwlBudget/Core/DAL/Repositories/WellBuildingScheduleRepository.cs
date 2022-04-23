using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Entities;
using Core.DAL.RepositoryInterfaces;
using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Core.DAL.Repositories;

public class WellBuildingScheduleRepository : Repository<WellBuildingScheduleItem, WellBuildingScheduleItemEntity>,
    IWellBuildingScheduleRepository
{
    public WellBuildingScheduleRepository(IDatabaseContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    public async Task<List<WellBuildingScheduleItem>> GetByProjectIdAsync(Guid projectId,
        CancellationToken cancellationToken)
    {
        var entities = await _context.Get<WellBuildingScheduleItemEntity>()
            .Include(_ => _.Well)
            .ThenInclude(_ => _.Cluster)
            .ThenInclude(_ => _.Lot)
            .Where(_ => _.Well.Cluster.Lot.ProjectId == projectId)
            .ToListAsync(cancellationToken);
        return _mapper.Map<List<WellBuildingScheduleItem>>(entities);
    }

    public async Task<List<WellBuildingScheduleItem>> GetByWellIdAsync(Guid wellId, CancellationToken cancellationToken)
    {
        var entities = await _context.Get<WellBuildingScheduleItemEntity>()
            .Where(_ => _.WellId == wellId)
            .ToListAsync(cancellationToken);
        return _mapper.Map<List<WellBuildingScheduleItem>>(entities);
    }
}