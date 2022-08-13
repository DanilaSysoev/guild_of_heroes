using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBiome : WorldArea, IWorldBiome
{
    private TileType tileType;
    public ITileType TilesType { get { return tileType; } }

    public WorldBiome(TileType tileType)
        : base()
    {
        this.tileType = tileType;
    }
}
