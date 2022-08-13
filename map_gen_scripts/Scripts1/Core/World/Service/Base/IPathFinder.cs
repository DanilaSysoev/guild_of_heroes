using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathFinder
{
    IPath GetPath(Vector2Int from, Vector2Int to, IWorld world);
    IPath GetPath(int fromX, int fromY, int toX, int toY, IWorld world);
    
    IPath GetPath(Vector2Int from, Vector2Int to, IWorld world, IReadOnlyList<ITileType> exceptTiles);
    IPath GetPath(int fromX, int fromY, int toX, int toY, IWorld world, IReadOnlyList<ITileType> exceptTiles);
}
