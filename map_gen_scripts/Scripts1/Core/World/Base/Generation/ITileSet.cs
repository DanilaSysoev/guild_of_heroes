using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITileSet
{
    IReadOnlyList<WorldTile> Tiles { get; }
    IReadOnlyList<ITilemapLevel> SuitableLevels { get; }    

    WorldTile GetRandomTile();
    bool CanBePlacedOn(ITilemapLevel level);
}
