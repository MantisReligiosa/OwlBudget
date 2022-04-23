using Core.BLL.DataCatalog.GetById;
using Core.BLL.DataCatalog.GetList;
using Core.DAL;
using Core.DAL.Repositories;
using Core.DAL.RepositoryInterfaces;
using Core.Domain;
using Core.Models;
using Core.ServiceInterfaces;
using Core.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OwlBudget;

public static class ContainerConfigurator
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        RegisterServices(services);
        RegisterRepositories(services);
        RegisterGenericHandlers(services);

        services.AddScoped<IDatabaseContext, DatabaseContext>();

        services.AddScoped<IConfigService, ConfigService>(_ => new ConfigService(
            configuration.GetConnectionString("database")));

        return services;
    }

    private static void RegisterGenericHandlers(IServiceCollection services)
    {
        RegisterDataCatalog<Project, ProjectRepository>(services);
        RegisterDataCatalog<DrillingRig, DrillingRigRepository>(services);
        RegisterDataCatalog<Region, RegionRepository>(services);
        RegisterDataCatalog<WellConstruction, WellConstrustionRepository>(services);
        RegisterDataCatalog<WellType, WellTypeRepository>(services);
    }

    private static void RegisterDataCatalog<TNamedIdentity, TRepository>(IServiceCollection services)
        where TNamedIdentity : NamedIdentity
        where TRepository : class, IRepository<TNamedIdentity>
    {
        services
            .AddTransient<IRequestHandler<GetByIdRequest<TNamedIdentity>, TNamedIdentity>,
                GetByIdHandler<TNamedIdentity>>();
        services
            .AddTransient<IRequestHandler<GetListRequest<TNamedIdentity>, PagedList<TNamedIdentity>>,
                GetListHandler<TNamedIdentity>>();
        services.AddScoped<IRepository<TNamedIdentity>, TRepository>();
    }

    private static void RegisterRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IRegionRepository, RegionRepository>();
        services.AddScoped<IWellRepository, WellRepository>();
        services.AddScoped<IWellTypeRepository, WellTypeRepository>();
        services.AddScoped<IWellConstructionRepository, WellConstrustionRepository>();
        services.AddScoped<IDrillingRigRepository, DrillingRigRepository>();
        services.AddScoped<IWellsToDrillingRigSequencesRepository, WellsToDrillingRigSequencesRepository>();
        services.AddScoped<IScenarioRepository, ScenarioRepository>();
        services.AddScoped<ILotRepository, LotRepository>();
        services.AddScoped<IClusterRepository, ClusterRepository>();
        services.AddScoped<IDrillingRigSequenceRepository, DrillingRigSequenceRepository>();
        services.AddScoped<IWellBuildingScheduleRepository, WellBuildingScheduleRepository>();
    }

    private static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICryptoService, CryptoService>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IRegionService, RegionService>();
        services.AddScoped<IWellTypeService, WellTypeService>();
        services.AddScoped<IWellConstructionService, WellConstructionService>();
        services.AddScoped<IDrillingRigService, DrillingRigService>();
    }
}