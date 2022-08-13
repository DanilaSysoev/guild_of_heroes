using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldTile : Tile, IWorldTile
{
    [SerializeField]
    private TileType tileType;
    public ITileType TileType { get { return tileType; } }
}
