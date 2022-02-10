using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondSquare
{
    private float offset;
    private float offsetDivider;

    private DiamondSquare(float offset, float offsetDivider)
    {
        this.offset = offset;
        this.offsetDivider = offsetDivider;
    }

    public float[,] Generate(int width, int height)
    {
        if (width <= 0 || height <= 0)
            throw new ArgumentException("Size must be positive");

        float[,] intermediateSquareResult =
            GenerateIntermediateSquare(GetPowerTwoPlusOneSize(width, height));

        return null;
    }

    private float[,] GenerateIntermediateSquare(int size)
    {
        float[,] res = new float[size, size];
        return null;
    }

    public static int GetPowerTwoPlusOneSize(int width, int height)
    {
        int maxSize = Mathf.Max(width, height);
        int pow2 = 1;
        while (pow2 + 1 < maxSize)
            pow2 <<= 1;
        return pow2 + 1;
    }
    public static DiamondSquare Create(float offset, float offsetDivider)
    {
        if (offset < 0)
            throw new ArgumentException("Offset can not be negative");
        if (offsetDivider < 1)
            throw new ArgumentException("Offset divider can not be < 1");

        return new DiamondSquare(offset, offsetDivider);
    }
}
