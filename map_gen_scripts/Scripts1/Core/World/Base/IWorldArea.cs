using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWorldArea
{
    IReadOnlyList<Vector2Int> TilesPositions { get; }

    bool IsIn(Vector2Int position);
}
