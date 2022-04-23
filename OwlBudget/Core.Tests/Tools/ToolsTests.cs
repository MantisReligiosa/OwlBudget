using Core.Tools;
using NUnit.Framework;

namespace Core.Tests.Tools;

public class ToolsTests
{
    [Test]
    public void EnumTest()
    {
        var result = EnumExtension.GetEnumItems<EnumTest>();
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual("First", result[0].Description);
        Assert.AreEqual(Tools.EnumTest.One, result[0].Value);
        Assert.AreEqual("Second", result[1].Description);
        Assert.AreEqual(Tools.EnumTest.Two, result[1].Value);
    }
}

public enum EnumTest
{
    [System.ComponentModel.Description("First")]
    One = 1,

    [System.ComponentModel.Description("Second")]
    Two = 2
}