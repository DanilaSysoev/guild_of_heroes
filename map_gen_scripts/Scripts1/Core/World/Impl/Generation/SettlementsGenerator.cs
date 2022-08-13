using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SettlementsGenerator : MonoBehaviour, IWorldGenerator
{
    [SerializeField]
    private int minSettlementsCount;
    [SerializeField]
    private int maxSettlementsCount;

    [SerializeField]
    private GameObject clearSettlementObject;
    [SerializeField]
    private GameObject clearSatelliteObject;

    [SerializeField]
    private List<SettlementGenerationPrototype> prototypes;
    [SerializeField]
    private List<float> prototypesWeights;
    [SerializeField]
    private List<int> minSettlementsCountByTypes;

    private Dictionary<ITileType, List<KeyValuePair<SettlementGenerationPrototype, float>>> prototypesByTilesDict;

    private void Awake()
    {
        prototypesByTilesDict = new Dictionary<ITileType, List<KeyValuePair<SettlementGenerationPrototype, float>>>();
        for(int i = 0; i < prototypes.Count; ++i)
        {            
            foreach(var tt in prototypes[i].SuitableTileTypes)
            {
                if (!prototypesByTilesDict.ContainsKey(tt))
                    prototypesByTilesDict.Add(tt, new List<KeyValuePair<SettlementGenerationPrototype, float>>());
                prototypesByTilesDict[tt].Add(new KeyValuePair<SettlementGenerationPrototype, float>(prototypes[i], prototypesWeights[i]));
            }
        }
    }

    public void Generate(IWorld world)
    {
        int settlementCount = Random.Range(minSettlementsCount, maxSettlementsCount + 1);
        List<Settlement> settlements = new List<Settlement>();
        var prots = new List<SettlementGenerationPrototype>(prototypes);
        var protWeights = new List<float>(prototypesWeights);

        int cnt = 0;

        for(int i = 0; i < minSettlementsCountByTypes.Count; ++i)
        {
            for(int j = 0; j < minSettlementsCountByTypes[i]; ++j, cnt++)
            {
                var prototype = prototypes[i];
                settlements.Add(GenerateSettlement(world, prototype));
            }
        }

        for (int i = cnt; i < settlementCount; ++i)
        {
            settlements.Add(GenerateSettlement(world, prots, protWeights));
        }

        (world as World).SetSettlements(settlements);
    }

    private Settlement GenerateSettlement(IWorld world, SettlementGenerationPrototype prototype)
    {
        bool success = false;
        GameObject settlementObject;
        Settlement settlement;

        success = CheckPrototype(world, success, prototype, out settlementObject, out settlement);

        if (success)
        {
            settlementObject.transform.parent = world.Tilemap.GetInstantiatedObject(new Vector3Int(settlement.Position.x, settlement.Position.y, 0)).transform;
            settlementObject.GetComponent<SpriteRenderer>().sprite = prototype.SuitableSprites[Random.Range(0, prototype.SuitableSprites.Count)];
            return settlement;
        }
        else
            throw new System.InvalidOperationException("Невозможно сгенерировать город из указанных прототипов");
    }

    private Settlement GenerateSettlement(IWorld world, List<SettlementGenerationPrototype> prototypes, List<float> prototypesWeights)
    {
        bool success = false;
        while (!success && prototypes.Count > 0)
        {
            var prototype = Randomizer.GetItemFromRow(prototypes, prototypesWeights);
            GameObject settlementObject;
            Settlement settlement;

            success = CheckPrototype(world, success, prototype, out settlementObject, out settlement);

            if (success)
            {
                settlementObject.transform.parent = world.Tilemap.GetInstantiatedObject(new Vector3Int(settlement.Position.x, settlement.Position.y, 0)).transform;
                settlementObject.GetComponent<SpriteRenderer>().sprite = prototype.SuitableSprites[Random.Range(0, prototype.SuitableSprites.Count)];
                return settlement;
            }

            prototypes.Remove(prototype);
        }

        throw new System.InvalidOperationException("Невозможно сгенерировать город из указанных прототипов");
    }

    private bool CheckPrototype(IWorld world, bool success, SettlementGenerationPrototype prototype, out GameObject settlementObject, out Settlement settlement)
    {
        List<ITileType> tileTypes = new List<ITileType>(prototype.SuitableTileTypes);
        settlementObject = null;
        settlement = null;
        while (tileTypes.Count > 0 && !success)
        {
            var tileType = tileTypes[Random.Range(0, tileTypes.Count)];
            var biomes = new List<IWorldBiome>(world.GetBiomes(tileType));
            while (biomes.Count > 0 && !success)
            {
                CheckBiome(world, ref success, prototype, ref settlementObject, ref settlement, biomes);
            }
            tileTypes.Remove(tileType);
        }

        return success;
    }
    private void CheckBiome(IWorld world, ref bool success, SettlementGenerationPrototype prototype, ref GameObject settlementObject, ref Settlement settlement, List<IWorldBiome> biomes)
    {
        var biome = biomes[Random.Range(0, biomes.Count)];
        if (biome.TilesPositions.Count >= prototype.SettlementType.Size)
        {
            var positions = new List<Vector2Int>(biome.TilesPositions);
            while (positions.Count > 0 && !success)
            {
                CheckPosition(world, ref success, prototype, ref settlementObject, ref settlement, positions);
            }
            biomes.Remove(biome);
        }
        else
        {
            biomes.Remove(biome);
        }
    }
    private void CheckPosition(IWorld world, ref bool success, SettlementGenerationPrototype prototype, ref GameObject settlementObject, ref Settlement settlement, List<Vector2Int> positions)
    {
        var pos = positions[Random.Range(0, positions.Count)];
        if (!IntersectWithOtherSettlement(world, pos, prototype.SettlementType.Size))
        {
            settlementObject = CreateSettlementTypeRacesClasses(prototype, world.Tilemap.GetInstantiatedObject(new Vector3Int(pos.x, pos.y, 0)));
            settlement = settlementObject.GetComponent<Settlement>();
            PlaceSettlement(world, prototype, settlement, pos);
            success = true;
        }
        else
            positions.Remove(pos);
    }

    private void PlaceSettlement(IWorld world, ISettlementGenerationPrototype prototype, Settlement settlement, Vector2Int pos)
    {
        var neighbors = world.GetNeighborsPositions(pos, settlement.SettlementType.Size);
        foreach (var n in neighbors)        
            world.GetTileInfo(n).Settlement = settlement;
        settlement.SetTiles(neighbors);
        settlement.SetTilesByDistance(world.GetNeighborsPositionsByDistance(pos, settlement.SettlementType.Size));
        settlement.SetPosition(pos);

        PlaceSatellites(world, prototype, settlement);
    }

    private void PlaceSatellites(IWorld world, ISettlementGenerationPrototype prototype, Settlement settlement)
    {
        HashSet<Vector2Int> satellitesPositions = new HashSet<Vector2Int>(world.GetNeighborsPositions(settlement.Position));
        float weight = 1;
        Dictionary<Vector2Int, float> spWeights = new Dictionary<Vector2Int, float>();
        foreach (var sp in satellitesPositions)
            spWeights.Add(sp, weight);

        Vector2Int pos = settlement.Position;
        List<Vector2Int> satPoses = new List<Vector2Int>();
        for (int i = 0; i < settlement.SettlementType.Size - 1; ++i)
        {
            satPoses.Add(pos);
            satellitesPositions.ExceptWith(satPoses);
            foreach(var sp in satPoses)
                spWeights.Remove(sp);

            var satellitesPositionsOld = new HashSet<Vector2Int>(satellitesPositions);

            satellitesPositions.RemoveWhere(sp => !prototype.SuitableTileTypes.Contains(((WorldTile)world.Tilemap.GetTile(new Vector3Int(sp.x, sp.y, 0))).TileType));

            satellitesPositionsOld.ExceptWith(satellitesPositions);
            foreach (var sp in satellitesPositionsOld)
                spWeights.Remove(sp);

            var sPoses = new List<Vector2Int>();
            var sPosesWeights = new List<float>();
            foreach(var pair in spWeights)
            {
                sPoses.Add(pair.Key);
                sPosesWeights.Add(pair.Value);
            }

            //if (sPoses.Count == 0)
            //    print("Oops!");
            pos = Randomizer.GetItemFromRow(sPoses, sPosesWeights);
            var sprite = prototype.SuitableSatellitesSprites[Random.Range(0, prototype.SuitableSatellitesSprites.Count)];

            var satObj = Instantiate(clearSatelliteObject, world.Tilemap.GetInstantiatedObject(new Vector3Int(pos.x, pos.y, 0)).transform);
            satObj.GetComponent<SpriteRenderer>().sprite = sprite;

            weight /= 3.0f;
            satellitesPositionsOld = new HashSet<Vector2Int>(satellitesPositions);

            satellitesPositions.UnionWith(world.GetNeighborsPositions(pos));
            satellitesPositions.Remove(settlement.Position);

            var tmp = new HashSet<Vector2Int>(satellitesPositions);
            satellitesPositions.ExceptWith(satellitesPositionsOld);
            foreach (var sp in satellitesPositions)
                spWeights.Add(sp, weight);
            satellitesPositions = tmp;
        }
    }

    private bool IntersectWithOtherSettlement(IWorld world, Vector2Int pos, int settlementSize)
    {
        var neighbors = world.GetNeighborsPositions(pos, settlementSize);
        foreach (var n in neighbors)
            if (world.GetTileInfo(n).Settlement != null)
                return true;

        return false;
    }

    private GameObject CreateSettlementTypeRacesClasses(ISettlementGenerationPrototype prototype, GameObject parent)
    {
        var settlementObject = Instantiate(clearSettlementObject, parent.transform);
        var settlement = settlementObject.GetComponent<Settlement>();

        settlement.SetSettlementType(prototype.SettlementType as SettlementType);

        settlement.SetMercenaryRaces(GetRaces(prototype));
        settlement.SetMercenaryClasses(GetClasses(prototype));

        return settlementObject;
    }

    private List<Race> GetRaces(ISettlementGenerationPrototype prototype)
    {
        List<Race> races = new List<Race>();

        int mainRacesCount = Random.Range(prototype.MinMainRacesCount, prototype.MaxMainRacesCount + 1);
        int secondaryRacesCount = Random.Range(0, prototype.MaxTotalRacesCount - mainRacesCount + 1);

        races.AddRange((IEnumerable<Race>)Randomizer.GetUniqueSetFromRow((List<Race>)prototype.MainRaces, prototype.MainRacesWeights, mainRacesCount));
        races.AddRange((IEnumerable<Race>)Randomizer.GetUniqueSetFromRow((List<Race>)prototype.SecondaryRaces, prototype.SecondaryRacesWeights, secondaryRacesCount));

        return races;
    }

    private List<Class> GetClasses(ISettlementGenerationPrototype prototype)
    {
        List<Class> classes = new List<Class>();

        int mainClassesCount = Random.Range(prototype.MinMainClassesCount, prototype.MaxMainClassesCount + 1);
        int secondaryClassesCount = Random.Range(0, prototype.MaxTotalClassesCount - mainClassesCount + 1);

        classes.AddRange((IEnumerable<Class>)Randomizer.GetUniqueSetFromRow((List<Class>)prototype.MainClasses, prototype.MainClassesWeights, mainClassesCount));
        classes.AddRange((IEnumerable<Class>)Randomizer.GetUniqueSetFromRow((List<Class>)prototype.SecondaryClasses, prototype.SecondaryClassesWeights, secondaryClassesCount));

        return classes;
    }
}
