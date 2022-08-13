using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tilemap Splitting", menuName = "Tilemap Splitting")]
public class TilemapSplitting : ScriptableObject, ITilemapSplitting
{
    [SerializeField]
    private List<TilemapLevel> tilemapLevels;
    public IReadOnlyList<ITilemapLevel> TilemapLevels { get { return tilemapLevels; } }

    [SerializeField]
    private List<int> relativeSizes;
    public IReadOnlyList<int> RelativeSizes { get { return relativeSizes; } }

    public int TotalSize { get { return relativeSizes.Sum(); } }

    private Dictionary<ITilemapLevel, int> sizesByLevel;

    public int GetLevelRelativeSize(ITilemapLevel level)
    {
        if (sizesByLevel.ContainsKey(level))
            return sizesByLevel[level];
        return 0;
    }

    public void Setup()
    {
        sizesByLevel = new Dictionary<ITilemapLevel, int>();
        for (int i = 0; i < tilemapLevels.Count; ++i)
            sizesByLevel.Add(tilemapLevels[i], relativeSizes[i]);
    }

    public bool IsValid()
    {
        for (int i = 0; i < tilemapLevels.Count - 1; ++i)
            if (tilemapLevels[i].RelativeHeight <= tilemapLevels[i + 1].RelativeHeight)
                return false;
        return true;
    }

    public float GetLevelPercentSize(ITilemapLevel level)
    {
        return (float)GetLevelRelativeSize(level) / TotalSize;
    }
}
