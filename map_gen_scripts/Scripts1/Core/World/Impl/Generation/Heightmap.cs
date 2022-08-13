using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heightmap : IHeightmap
{
    private int[,] heights;

    public int SizeX { get { return heights.GetLength(1); } }
    public int SizeY { get { return heights.GetLength(0); } }

    private int minHeight;
    public int MinHeightValue { get { return minHeight; } }

    private int maxHeight;
    public int MaxHeightValue { get { return maxHeight; } }

    public Heightmap(int[,] heights)
    {
        this.heights = (int[,])heights.Clone();
        minHeight = GetMinHeight();
        maxHeight = GetMaxHeight();
    }

    public IHeightmap Clone()
    {
        return new Heightmap(heights);
    }

    public IHeightStatList GetHeightStatList(int step)
    {
        return new HeightStatList(step, minHeight, maxHeight, heights);
    }

    public int GetHeightValue(int x, int y)
    {
        return heights[y, x];
    }
    
    private int GetMinHeight()
    {
        int min = heights[0, 0];
        foreach (var h in heights)
            if (h < min)
                min = h;
        return min;
    }
    public int GetMaxHeight()
    {
        int max = heights[0, 0];
        foreach (var h in heights)
            if (h > max)
                max = h;
        return max;
    }



    public void TMP_SaveToTexture_REMOVE_LATER(string filename)
    {
        Texture2D t = new Texture2D(SizeX, SizeY);

        for (int i = 0; i < SizeY; ++i)
            for (int j = 0; j < SizeX; ++j)
                t.SetPixel(j, i, new Color(
                    ((float)heights[i, j] - MinHeightValue) / (MaxHeightValue - MinHeightValue),
                    ((float)heights[i, j] - MinHeightValue) / (MaxHeightValue - MinHeightValue),
                    ((float)heights[i, j] - MinHeightValue) / (MaxHeightValue - MinHeightValue)));

        var png = t.EncodeToPNG();
        using (var fs = new System.IO.FileStream(filename, System.IO.FileMode.OpenOrCreate))
            fs.Write(png, 0, png.Length);
    }
}
