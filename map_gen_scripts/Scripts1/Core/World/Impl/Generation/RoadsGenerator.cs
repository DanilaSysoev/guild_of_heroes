using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadsGenerator : MonoBehaviour, IWorldGenerator
{
    [SerializeField]
    private List<SettlementType> centerTypes;
    [SerializeField]
    private List<TileType> exceptTileTypes;

    [SerializeField]
    private GameObject clearRoadObject;

    [SerializeField]
    int roadSpriteGroupsCount;
    [SerializeField]
    [Tooltip("Заполнять по индексам в соответствии с битовой маской спрайта дороги (от северо-запада по часовой стрелке) и номером группы\nЕсли число групп не равно степени двойки, то часть ячеек - пустая.")]
    private List<Sprite> roadSprites;

    [SerializeField]
    [Range(0, 1)]
    [Tooltip("Отсекающий коеффициент между минимальным и максимальным расстояниями от города для путей")]
    float pathLengthCoefficient = .3f;

    public void Generate(IWorld world)
    {
        List<ISettlement> centers = GetCenters(world);

        HashSet<Vector2Int> roadPositions = new HashSet<Vector2Int>();

        Dictionary<Vector2Int, GameObject> roads = new Dictionary<Vector2Int, GameObject>();

        Dictionary<ISettlement, Dictionary<ISettlement, bool>> pathesExist = new Dictionary<ISettlement, Dictionary<ISettlement, bool>>();

        List<IPath> allPathes = new List<IPath>();

        foreach(var s in world.Settlements)
            pathesExist.Add(s, new Dictionary<ISettlement, bool>());

        foreach(var s in world.Settlements)
        {
            List<IPath> pths = new List<IPath>();
            int minLength = int.MaxValue;
            int maxLength = int.MinValue;
            foreach(var ss in world.Settlements)
            {
                if (s != ss && !pathesExist[ss].ContainsKey(s))
                {
                    var p = world.GetPath(s.Position, ss.Position, exceptTileTypes);
                    if (p != null)
                    {
                        pths.Add(p);
                        if (p.Points.Count > maxLength)
                            maxLength = p.Points.Count;
                        if (p.Points.Count < minLength)
                            minLength = p.Points.Count;
                        pathesExist[s].Add(ss, true);
                    }
                }
            }

            foreach(var p in pths)
            {
                if (p.Points.Count <= minLength + (maxLength - minLength) * pathLengthCoefficient)
                    allPathes.Add(p);
            }
        }

        foreach (var p in allPathes)
            ProcessPath(world, roadPositions, roads, p);

        foreach(var p in roadPositions)
        {
            var tgo = world.Tilemap.GetInstantiatedObject(new Vector3Int(p.x, p.y, 0));
            var roadObject = roads[p];
            var road = roadObject.GetComponent<Road>();
            world.GetTileInfo(p).Road = road;

            var roadSprite = CalcRoadSprite(road);

            roadObject.GetComponent<SpriteRenderer>().sprite = roadSprite;
        }
    }

    private void ProcessPath(IWorld world, HashSet<Vector2Int> roadPositions, Dictionary<Vector2Int, GameObject> roads, IPath path)
    {
        for (int i = 0; i < path.Points.Count; ++i)
        {
            var p = path.Points[i];

            if (!roadPositions.Contains(p))
            {
                var tgo = world.Tilemap.GetInstantiatedObject(new Vector3Int(p.x, p.y, 0));
                var roadObject = Instantiate(clearRoadObject, tgo.transform);
                roads.Add(p, roadObject);
            }
            var road = roads[p].GetComponent<Road>();
            if (i > 0)
                road.AddDirection(Direction.GetDirection(p, path.Points[i - 1]));
            if (i < path.Points.Count - 1)
                road.AddDirection(Direction.GetDirection(p, path.Points[i + 1]));

            roadPositions.Add(p);
        }
    }

    private Sprite CalcRoadSprite(Road road)
    {
        int mask = road.GetMask();

        int spriteIndex = PrepareIndex(mask);
        return roadSprites[spriteIndex];
    }

    private int PrepareIndex(int mask)
    {
        int rgc = roadSpriteGroupsCount;
        int group = UnityEngine.Random.Range(0, rgc);
        int m = mask;
        while(rgc - 1 > 0)
        {
            rgc >>= 1;
            m <<= 1;
        }
        m |= group;
        return m;
    }

    private List<ISettlement> GetCenters(IWorld world)
    {
        List<ISettlement> centers = new List<ISettlement>();
        foreach (var st in centerTypes)
            centers.AddRange(world.GetSettlementsByType(st));
        return centers;
    }
}
