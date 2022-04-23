using AutoMapper;
using Core.Domain;
using NUnit.Framework;
using OwlBudget;

namespace Core.Tests;

public class MapperTest
{
    [Test]
    public void AssertingMapping()
    {
        var mapperConfiguration = new MapperConfiguration(_ => { _.AddMaps(typeof(Startup), typeof(Project)); });

        mapperConfiguration.AssertConfigurationIsValid();
    }
}