using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Entities;
using Core.DAL.RepositoryInterfaces;
using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Core.DAL.Repositories;

public class ProjectRepository : Repository<Project, ProjectEntity>, IProjectRepository
{
    public ProjectRepository(IDatabaseContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    public async Task<bool> IsNameInUseAsync(string name, Guid currentProjectId, CancellationToken cancellationToken)
    {
        return await _context.Get<ProjectEntity>(_ => _.Name == name && _.Id != currentProjectId)
            .AnyAsync(cancellationToken);
    }

    public override async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Get<ProjectEntity>()
            .Include(_ => _.Lots)
            .ThenInclude(_ => _.Clusters)
            .ThenInclude(_ => _.WellType)
            .Include(_ => _.Lots)
            .ThenInclude(_ => _.Clusters)
            .ThenInclude(_ => _.Wells)
            .ThenInclude(_ => _.WellsToDrillingRigSequences)
            .Include(_ => _.Scenarios)
            .SingleOrDefaultAsync(_ => _.Id == id, cancellationToken);
        if (entity != null)
        {
            _context.RemoveEntity(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<Project> GetByClusterIdAsync(Guid clusterId, CancellationToken cancellationToken)
    {
        var query = from p in _context.Get<ProjectEntity>()
            join l in _context.Get<LotEntity>() on p.Id equals l.ProjectId
            join c in _context.Get<ClusterEntity>() on l.Id equals c.LotId
            select new {Project = p, Cluster = c};

        var entity = (await query.SingleOrDefaultAsync(_ => _.Cluster.Id == clusterId, cancellationToken))?.Project;

        return _mapper.Map<Project>(entity);
    }
}