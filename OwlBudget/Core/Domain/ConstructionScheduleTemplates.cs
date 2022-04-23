using System.Collections.Immutable;
using System.Linq;

namespace Core.Domain;

public static class ConstructionScheduleTemplates
{
    public enum ConstructionScheduleTemplateTypes
    {
        /// <summary>
        /// ПЗР
        /// </summary>
        Preparatory = 1,
        /// <summary>
        /// ВМР (монтаж)
        /// </summary>
        Installation = 2,
        /// <summary>
        /// ВМР (демонтаж)
        /// </summary>
        Uninstallation = 3,
        /// <summary>
        /// ГГД
        /// </summary>
        DepthDayGraph = 4,
        /// <summary>
        /// Передвижка
        /// </summary>
        Moving = 5,
        /// <summary>
        /// Мобилизация
        /// </summary>
        Mobilization = 6,
        /// <summary>
        /// Демобилизация
        /// </summary>
        Demobilization = 7
    }

    public static ImmutableArray<ConstructionScheduleTemplateType> All = ImmutableArray.Create(
        new ConstructionScheduleTemplateType(ConstructionScheduleTemplateTypes.Preparatory, "ПЗР", true),
        new ConstructionScheduleTemplateType(ConstructionScheduleTemplateTypes.Installation, "ВМР (монтаж)", false),
        new ConstructionScheduleTemplateType(ConstructionScheduleTemplateTypes.Uninstallation, "ВМР (демонтаж)", false),
        new ConstructionScheduleTemplateType(ConstructionScheduleTemplateTypes.DepthDayGraph, "ГГД", false),
        new ConstructionScheduleTemplateType(ConstructionScheduleTemplateTypes.Moving, "Передвижение", false),
        new ConstructionScheduleTemplateType(ConstructionScheduleTemplateTypes.Mobilization, "Мобилизац", true),
        new ConstructionScheduleTemplateType(ConstructionScheduleTemplateTypes.Demobilization, "Демобилизац", true));

    public static ConstructionScheduleTemplateType ByType(ConstructionScheduleTemplateTypes type)
    {
        return All.FirstOrDefault(_ => _.TemplateType == type);
    }

    public static ConstructionScheduleTemplateType ByType(int type)
    {
        return ByType((ConstructionScheduleTemplateTypes) type);
    }

    public static bool HaveVariableLatency(ConstructionScheduleTemplateTypes type)
    {
        return ByType(type).HaveVariableLatency;
    }

    public record ConstructionScheduleTemplateType(ConstructionScheduleTemplateTypes TemplateType, string Description,
        bool HaveVariableLatency);
}