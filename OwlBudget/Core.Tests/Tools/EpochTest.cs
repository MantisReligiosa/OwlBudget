using System;
using Core.Tools;
using NUnit.Framework;

namespace Core.Tests.Tools;

public class EpochTest
{
    [Test]
    public void Test()
    {
        var date = new DateTime(1985, 01, 22, 15, 30, 30);
        var epoch = date.ToEpoch();
        var convertedBack = DateTimeExtension.FromEpoch(epoch);
        Assert.AreEqual(date, convertedBack);
    }
}