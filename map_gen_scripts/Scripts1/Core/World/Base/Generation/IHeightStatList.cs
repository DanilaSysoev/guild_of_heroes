using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHeightStatList
{
    int Step { get; }
    int MinHeight { get; }
    int MaxHeight { get; }
    int TotalCount { get; }

    int GetCount(int level);
    float GetStat(int level);
}
