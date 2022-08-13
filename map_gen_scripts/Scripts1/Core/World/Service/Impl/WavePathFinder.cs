using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WavePathFinder : MonoBehaviour, IPathFinder
{
    private Vector2Int GetMinCandidate(Dictionary<Vector2Int, float> candidates)
    {
        float minValue = float.MaxValue;
        Vector2Int candidate = Vector2Int.zero;
        foreach(var pair in candidates)
        {
            if(pair.Value < minValue)
            {
                minValue = pair.Value;
                candidate = pair.Key;
            }
        }
        return candidate;
    }

    public IPath GetPath(Vector2Int from, Vector2Int to, IWorld world)
    {
        return GetPath(from, to, world, new List<ITileType>());
    }

    private float GetPassability(IWorld world, Vector2Int minCand, IReadOnlyList<ITileType> exceptTiles)
    {
        World w = world as World;
        var tile = (IWorldTile)w.Tilemap.GetTile(new Vector3Int(minCand.x, minCand.y, 0));
        if (tile != null && exceptTiles.Contains(tile.TileType) || tile == null)
            return 0;

        if (w.GetTileInfo(minCand) != null && w.GetTileInfo(minCand).Road != null)
            return WorldConstants.Instance.RoadPassability;

        return tile.TileType.Passability;
    }

    public IPath GetPath(int fromX, int fromY, int toX, int toY, IWorld world)
    {
        return GetPath(new Vector2Int(fromX, fromY), new Vector2Int(toX, toY), world);
    }

    public IPath GetPath(Vector2Int from, Vector2Int to, IWorld world, IReadOnlyList<ITileType> exceptTiles)
    {
        float fromPassability = GetPassability(world, from, exceptTiles);
        float toPassability = GetPassability(world, to, exceptTiles);

        if (fromPassability == 0 || toPassability == 0)
            return null;

        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
        Dictionary<Vector2Int, float> candidates = new Dictionary<Vector2Int, float>();
        Dictionary<Vector2Int, Vector2Int> parents = new Dictionary<Vector2Int, Vector2Int>();

        candidates.Add(from, 0);
        Vector2Int minCand = Vector2Int.zero;

        while (candidates.Count > 0)
        {
            minCand = GetMinCandidate(candidates);
            float minCandPassability = GetPassability(world, minCand, exceptTiles);

            visited.Add(minCand);
            if (minCand == to)// || candidates.ContainsKey(minCand) && float.IsPositiveInfinity(candidates[minCand]))
                break;

            if (minCandPassability > 0)
            {
                var neighbors = world.GetNeighborsPositions(minCand);
                foreach (var neighbor in neighbors)
                {
                    if (!visited.Contains(neighbor))
                    {
                        float dist = 0;
                        if (minCandPassability > 0)
                            dist = candidates[minCand] + 1 / minCandPassability;
                        else
                            dist = candidates[minCand] + float.PositiveInfinity;

                        if (!candidates.ContainsKey(neighbor))
                        {
                            candidates.Add(neighbor, dist);
                            parents.Add(neighbor, minCand);
                        }

                        if (candidates[neighbor] > dist)
                        {
                            candidates[neighbor] = dist;
                            parents[neighbor] = minCand;
                        }

                    }
                }
            }
            

            candidates.Remove(minCand);
        }

        if (minCand != to)
            return null;

        List<Vector2Int> path = new List<Vector2Int>();
        while (minCand != from)
        {
            path.Add(minCand);
            minCand = parents[minCand];
        }
        path.Add(minCand);
        path.Reverse();

        return new Path(path);
    }

    public IPath GetPath(int fromX, int fromY, int toX, int toY, IWorld world, IReadOnlyList<ITileType> exceptTiles)
    {
        return GetPath(new Vector2Int(fromX, fromY), new Vector2Int(toX, toY), world, exceptTiles);
    }
}
