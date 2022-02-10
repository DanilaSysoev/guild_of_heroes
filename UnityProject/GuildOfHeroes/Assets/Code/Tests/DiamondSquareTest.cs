using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DiamondSquareTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void GetPowerTwoPlusOneSize_WidthGreaterThanHeightNonPowerTwo_ResultOk()
    {
        int res = DiamondSquare.GetPowerTwoPlusOneSize(10, 5);
        Assert.AreEqual(17, res);
    }
    [Test]
    public void GetPowerTwoPlusOneSize_WidthLessThanHeightNonPowerTwo_ResultOk()
    {
        int res = DiamondSquare.GetPowerTwoPlusOneSize(5, 10);
        Assert.AreEqual(17, res);
    }
    [Test]
    public void GetPowerTwoPlusOneSize_WidthEqualsHeightNonPowerTwo_ResultOk()
    {
        int res = DiamondSquare.GetPowerTwoPlusOneSize(12, 12);
        Assert.AreEqual(17, res);
    }
    [Test]
    public void GetPowerTwoPlusOneSize_WidthGreatestSizeIsPwerTwo_ResultOk()
    {
        int res = DiamondSquare.GetPowerTwoPlusOneSize(16, 5);
        Assert.AreEqual(17, res);
    }
    [Test]
    public void GetPowerTwoPlusOneSize_HeightGreatestSizeIsPwerTwo_ResultOk()
    {
        int res = DiamondSquare.GetPowerTwoPlusOneSize(5, 16);
        Assert.AreEqual(17, res);
    }
    [Test]
    public void GetPowerTwoPlusOneSize_WidthEqualsHeightPowerTwo_ResultOk()
    {
        int res = DiamondSquare.GetPowerTwoPlusOneSize(32, 32);
        Assert.AreEqual(33, res);
    }
    [Test]
    public void GetPowerTwoPlusOneSize_WidthGreatestSizeAlreadyResult_ResultOk()
    {
        int res = DiamondSquare.GetPowerTwoPlusOneSize(17, 5);
        Assert.AreEqual(17, res);
    }
    [Test]
    public void GetPowerTwoPlusOneSize_HeightGreatestSizeAlreadyResult_ResultOk()
    {
        int res = DiamondSquare.GetPowerTwoPlusOneSize(5, 17);
        Assert.AreEqual(17, res);
    }
    [Test]
    public void GetPowerTwoPlusOneSize_WidthEqualsHeightAlreadyResult_ResultOk()
    {
        int res = DiamondSquare.GetPowerTwoPlusOneSize(33, 33);
        Assert.AreEqual(33, res);
    }

    [Test]
    public void Creation_OffsetNegative_ThrowsException()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => DiamondSquare.Create(-1, 2)
        );

        Assert.IsTrue(exception.Message.ToLower().Contains("offset can not be negative"));
    }
    [Test]
    public void Creation_OffsetDividerLessThanOne_ThrowsException()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => DiamondSquare.Create(1, .5f)
        );

        Assert.IsTrue(exception.Message.ToLower().Contains("offset divider can not be < 1"));
    }

    [Test]
    public void Generate_BothSizeIsNegative_ThrowsException()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => DiamondSquare.Create(1, 2).Generate(-1, -2)
        );

        Assert.IsTrue(exception.Message.ToLower().Contains("size must be positive"));
    }
    [Test]
    public void Generate_WidthIsNegative_ThrowsException()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => DiamondSquare.Create(1, 2).Generate(-2, 5)
        );

        Assert.IsTrue(exception.Message.ToLower().Contains("size must be positive"));
    }
    [Test]
    public void Generate_HeightIsNegative_ThrowsException()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => DiamondSquare.Create(1, 2).Generate(2, -5)
        );

        Assert.IsTrue(exception.Message.ToLower().Contains("size must be positive"));
    }
    [Test]
    public void Generate_BothSizeIsZero_ThrowsException()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => DiamondSquare.Create(1, 2).Generate(0, 0)
        );

        Assert.IsTrue(exception.Message.ToLower().Contains("size must be positive"));
    }
    [Test]
    public void Generate_WidthIsZero_ThrowsException()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => DiamondSquare.Create(1, 2).Generate(0, 5)
        );

        Assert.IsTrue(exception.Message.ToLower().Contains("size must be positive"));
    }
    [Test]
    public void Generate_HeightIsZero_ThrowsException()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => DiamondSquare.Create(1, 2).Generate(2, 0)
        );

        Assert.IsTrue(exception.Message.ToLower().Contains("size must be positive"));
    }


}
