using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class World : MonoBehaviour, IWorld
{
    public static IWorld Instance { get; private set; }

    [SerializeField]
    private Tilemap tilemap;
    public Tilemap Tilemap { get { return tilemap; } }

    [SerializeField]
    private WavePathFinder pathFinder;

    public int SizeX { get { return Tilemap.size.x; } }
    public int SizeY { get { return Tilemap.size.y; } }

    private List<Settlement> settlements;
    private Dictionary<ISettlementType, List<ISettlement>> settlementsByType;    
    public IReadOnlyList<ISettlement> Settlements { get { return settlements; } }
    internal void SetSettlements(List<Settlement> settlements)
    {
        this.settlements = settlements;
        settlementsByType = new Dictionary<ISettlementType, List<ISettlement>>();
        foreach (var s in settlements)
        {
            if (!settlementsByType.ContainsKey(s.SettlementType))
                settlementsByType.Add(s.SettlementType, new List<ISettlement>());
            settlementsByType[s.SettlementType].Add(s);
        }
    }
    public IReadOnlyList<ISettlement> GetSettlementsByType(ISettlementType settlementType)
    {
        if (settlementsByType.ContainsKey(settlementType))
            return settlementsByType[settlementType];
        return new List<ISettlement>();
    }

    private Dictionary<ITileType, IReadOnlyList<IWorldBiome>> biomes;
    internal void SetBiomes(Dictionary<ITileType, IReadOnlyList<IWorldBiome>> biomes) { this.biomes = biomes; }    

    private void Awake()
    {
        Instance = this;
    }

    public void SetSize(int sizeX, int sizeY)
    {
        tilemap.size = new Vector3Int(sizeX, sizeY, 0);
    }

    public IWorldTileInfo GetTileInfo(int x, int y)
    {
        var obj = Tilemap.GetInstantiatedObject(new Vector3Int(x, y, 0));
        if(obj != null)
            return obj.GetComponent<WorldTileInfo>();
        return null;
    }
    public IWorldTileInfo GetTileInfo(Vector2Int pos)
    {
        return GetTileInfo(pos.x, pos.y);
    }
    public IWorldTileInfo GetNeighborTileInfo(int x, int y, IDirection direction)
    {
        return GetTileInfo(direction.GetPositionInDirection(x, y));
    }
    public IWorldTileInfo GetNeighborTileInfo(Vector2Int pos, IDirection direction)
    {
        return GetNeighborTileInfo(pos.x, pos.y, direction);
    }

    public IPath GetPath(int fromX, int fromY, int toX, int toY)
    {
        return pathFinder.GetPath(fromX, fromY, toX, toY, this);
    }
    public IPath GetPath(Vector2Int from, Vector2Int to)
    {
        return pathFinder.GetPath(from, to, this);
    }
    public IPath GetPath(int fromX, int fromY, int toX, int toY, IReadOnlyList<ITileType> exceptTiles)
    {
        return pathFinder.GetPath(fromX, fromY, toX, toY, this, exceptTiles);
    }
    public IPath GetPath(Vector2Int from, Vector2Int to, IReadOnlyList<ITileType> exceptTiles)
    {
        return pathFinder.GetPath(from, to, this, exceptTiles);
    }

    public List<IWorldTileInfo> GetNeighbors(int x, int y)
    {
        var neiPoses = GetNeighborsPositions(x, y);
        List<IWorldTileInfo> infos = new List<IWorldTileInfo>();
        foreach (var pos in neiPoses)
            infos.Add(GetTileInfo(pos));
        return infos;
    }
    public List<IWorldTileInfo> GetNeighbors(Vector2Int pos)
    {
        var neiPoses = GetNeighborsPositions(pos);
        List<IWorldTileInfo> infos = new List<IWorldTileInfo>();
        foreach (var p in neiPoses)
            infos.Add(GetTileInfo(p));
        return infos;
    }

    public List<IWorldTileInfo> GetNeighbors(int x, int y, int distance)
    {
        var neiPoses = GetNeighborsPositions(x, y, distance);
        List<IWorldTileInfo> infos = new List<IWorldTileInfo>();
        foreach (var pos in neiPoses)
            infos.Add(GetTileInfo(pos));
        return infos;
    }
    public List<IWorldTileInfo> GetNeighbors(Vector2Int pos, int distance)
    {
        var neiPoses = GetNeighborsPositions(pos, distance);
        List<IWorldTileInfo> infos = new List<IWorldTileInfo>();
        foreach (var p in neiPoses)
            infos.Add(GetTileInfo(p));
        return infos;
    }

    public List<List<IWorldTileInfo>> GetNeighborsByDistance(int x, int y, int distance)
    {
        var neiPosByDist = GetNeighborsPositionsByDistance(x, y, distance);
        List<List<IWorldTileInfo>> result = new List<List<IWorldTileInfo>>();
        foreach(var posList in neiPosByDist)
        {
            var infos = new List<IWorldTileInfo>();
            foreach (var pos in posList)
                infos.Add(GetTileInfo(pos));
            result.Add(infos);
        }
        return result;
    }
    public List<List<IWorldTileInfo>> GetNeighborsByDistance(Vector2Int pos, int distance)
    {
        var neiPosByDist = GetNeighborsPositionsByDistance(pos, distance);
        List<List<IWorldTileInfo>> result = new List<List<IWorldTileInfo>>();
        foreach (var posList in neiPosByDist)
        {
            var infos = new List<IWorldTileInfo>();
            foreach (var p in posList)
                infos.Add(GetTileInfo(p));
            result.Add(infos);
        }
        return result;
    }

    public List<Vector2Int> GetNeighborsPositions(int x, int y)
    {
        List<Vector2Int> poses = new List<Vector2Int>();        
        CheckCorrectionAndAdd(x - 1, y, poses);
        CheckCorrectionAndAdd(x - 1 + y % 2, y + 1, poses);
        CheckCorrectionAndAdd(x + y % 2, y + 1, poses);
        CheckCorrectionAndAdd(x + 1, y, poses);
        CheckCorrectionAndAdd(x + y % 2, y - 1, poses);
        CheckCorrectionAndAdd(x - 1 + y % 2, y - 1, poses);
        return poses;
    }
    public List<Vector2Int> GetNeighborsPositions(Vector2Int pos)
    {
        return GetNeighborsPositions(pos.x, pos.y);
    }
    public List<Vector2Int> GetNeighborsPositions(int x, int y, int distance)
    {
        List<Vector2Int> res = new List<Vector2Int>();
        var nbp = GetNeighborsPositionsByDistance(x, y, distance);
        for (int i = 0; i < nbp.Count; ++i)
            res.AddRange(nbp[i]);

        return res;
    }
    public List<Vector2Int> GetNeighborsPositions(Vector2Int pos, int distance)
    {
        return GetNeighborsPositions(pos.x, pos.y, distance);
    }

    public List<List<Vector2Int>> GetNeighborsPositionsByDistance(int x, int y, int distance)
    {
        List<List<Vector2Int>> res = new List<List<Vector2Int>>();
        res.Add(new List<Vector2Int> { new Vector2Int(x, y) });
        res.Add(GetNeighborsPositions(x, y));        
        for(int i = 1; i < distance; ++i)
        {
            HashSet<Vector2Int> pastLayer = new HashSet<Vector2Int>(res[i]);
            HashSet<Vector2Int> layer = new HashSet<Vector2Int>();
            foreach (var p in pastLayer)
                layer.UnionWith(GetNeighborsPositions(p));
            layer.ExceptWith(pastLayer);
            layer.Remove(new Vector2Int(x, y));
            res.Add(new List<Vector2Int>(layer));
        }
        return res;
    }
    public List<List<Vector2Int>> GetNeighborsPositionsByDistance(Vector2Int pos, int distance)
    {
        return GetNeighborsPositionsByDistance(pos.x, pos.y, distance);
    }

    private void CheckCorrectionAndAdd(int x, int y, List<Vector2Int> list)
    {
        if (x >= 0 &&
            x < SizeX &&
            y >= 0 &&
            y < SizeY)
            list.Add(new Vector2Int(x, y));
    }

    public IReadOnlyList<IWorldBiome> GetBiomes(ITileType tileType)
    {
        if (biomes.ContainsKey(tileType))
            return biomes[tileType];

        return new List<IWorldBiome>();
    }

    public IWorldBiome GetBiome(Vector2Int position)
    {
        var tile = (WorldTile)Tilemap.GetTile(new Vector3Int(position.x, position.y, 0));
        var biomes = GetBiomes(tile.TileType);
        foreach (var b in biomes)
            if (b.IsIn(position))
                return b;
        return null;
    }

    public IEnumerable<ITileType> GetAllTileTypes()
    {
        return biomes.Keys;
    }

    public bool ContainsTile(ITileType tileType)
    {
        return biomes.ContainsKey(tileType);
    }
}
