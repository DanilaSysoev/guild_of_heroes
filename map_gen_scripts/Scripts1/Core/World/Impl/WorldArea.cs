using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldArea : IWorldArea
{
    private List<Vector2Int> tilesPositions;
    public IReadOnlyList<Vector2Int> TilesPositions { get { return tilesPositions; } }

    public WorldArea()
    {
        tilesPositions = new List<Vector2Int>();
    }

    public void AddPosition(Vector2Int position)
    {
        tilesPositions.Add(position);
    }

    public bool IsIn(Vector2Int position)
    {
        return tilesPositions.Contains(position);
    }
}
