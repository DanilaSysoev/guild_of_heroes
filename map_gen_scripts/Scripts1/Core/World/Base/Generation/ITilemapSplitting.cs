using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITilemapSplitting
{
    IReadOnlyList<ITilemapLevel> TilemapLevels { get; }
    IReadOnlyList<int> RelativeSizes { get; }
    int TotalSize { get; }

    void Setup();

    bool IsValid();

    int GetLevelRelativeSize(ITilemapLevel level);
    float GetLevelPercentSize(ITilemapLevel level);
}
