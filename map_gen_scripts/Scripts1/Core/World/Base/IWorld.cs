using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public interface IWorld
{
    int SizeX { get; }
    int SizeY { get; }
    Tilemap Tilemap { get; }
    IReadOnlyList<ISettlement> Settlements { get; }    

    IReadOnlyList<IWorldBiome> GetBiomes(ITileType tileType);
    IWorldBiome GetBiome(Vector2Int position);
    IEnumerable<ITileType> GetAllTileTypes();
    bool ContainsTile(ITileType tileType);
    IReadOnlyList<ISettlement> GetSettlementsByType(ISettlementType settlementType);

    IWorldTileInfo GetTileInfo(int x, int y);
    IWorldTileInfo GetTileInfo(Vector2Int pos);

    IWorldTileInfo GetNeighborTileInfo(int x, int y, IDirection direction);
    IWorldTileInfo GetNeighborTileInfo(Vector2Int pos, IDirection direction);

    void SetSize(int sizeX, int sizeY);

    IPath GetPath(int fromX, int fromY, int toX, int toY);
    IPath GetPath(Vector2Int from, Vector2Int to);
    IPath GetPath(int fromX, int fromY, int toX, int toY, IReadOnlyList<ITileType> exceptTiles);
    IPath GetPath(Vector2Int from, Vector2Int to, IReadOnlyList<ITileType> exceptTiles);

    List<IWorldTileInfo> GetNeighbors(int x, int y);
    List<IWorldTileInfo> GetNeighbors(Vector2Int pos);

    List<IWorldTileInfo> GetNeighbors(int x, int y, int distance);
    List<IWorldTileInfo> GetNeighbors(Vector2Int pos, int distance);

    List<List<IWorldTileInfo>> GetNeighborsByDistance(int x, int y, int distance);
    List<List<IWorldTileInfo>> GetNeighborsByDistance(Vector2Int pos, int distance);

    List<Vector2Int> GetNeighborsPositions(int x, int y);
    List<Vector2Int> GetNeighborsPositions(Vector2Int pos);

    List<Vector2Int> GetNeighborsPositions(int x, int y, int distance);
    List<Vector2Int> GetNeighborsPositions(Vector2Int pos, int distance);

    List<List<Vector2Int>> GetNeighborsPositionsByDistance(int x, int y, int distance);
    List<List<Vector2Int>> GetNeighborsPositionsByDistance(Vector2Int pos, int distance);

}
