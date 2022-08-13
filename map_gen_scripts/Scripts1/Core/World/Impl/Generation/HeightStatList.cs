using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightStatList : IHeightStatList
{
    private int step;
    public int Step { get { return step; } }

    private int minHeight;
    public int MinHeight { get { return minHeight; } }

    private int maxHeight;
    public int MaxHeight { get { return maxHeight; } }

    private int totalCount;
    public int TotalCount{ get { return totalCount; } }
    private Dictionary<int, int> countsByStep;

    public HeightStatList(int step, int minHeight, int maxHeight, int[,] data)
    {
        countsByStep = new Dictionary<int, int>();
        this.minHeight = minHeight;
        this.maxHeight = maxHeight;
        this.step = step;
        this.totalCount = data.Length;

        for (int i = 0; i <= maxHeight - minHeight; i += step)
            countsByStep.Add(i / step, 0);

        foreach (var h in data)
            countsByStep[(h - minHeight) / step]++;
    }

    public int GetCount(int level)
    {
        int key = (level - minHeight) / step;
        if (countsByStep.ContainsKey(key))
            return countsByStep[key];
        return 0;
    }

    public float GetStat(int level)
    {
        return (float)GetCount(level) / totalCount;
    }
}
