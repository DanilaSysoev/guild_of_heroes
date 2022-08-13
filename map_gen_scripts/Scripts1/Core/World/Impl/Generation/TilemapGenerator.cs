using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGenerator : MonoBehaviour, IWorldGenerator
{
    [SerializeField]
    List<TileSet> biomes;
    [SerializeField]
    List<int> maxBiomeSizes;
    [SerializeField]
    IntegerDiamondSquareHeightmapBuilder heightMapBuilder;
    [SerializeField]
    TilemapSplitting tilemapSplitting;
    [SerializeField]
    int heightStatListStep = 10;

    private void Awake()
    {
        tilemapSplitting.Setup();
        if (!tilemapSplitting.IsValid())
            throw new System.FormatException("TilemapSplitting is incorrect");
    }

    //private void TMP_PRINT_LEVELS(Dictionary<ITilemapLevel, Range> levelHeights)
    //{
    //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //    foreach (var lh in levelHeights)
    //    {
    //        sb.Append(string.Format("{0}-{1}..{2}\n", ((TilemapLevel)lh.Key).name, lh.Value.min, lh.Value.max));
    //    }
    //    Debug.Log(sb.ToString());
    //}

    #region QUEUE
    public void Generate(IWorld world)
    {
        Tilemap clearTilemap = world.Tilemap;
        clearTilemap.RefreshAllTiles();

        var heightmap = heightMapBuilder.BuildHeightMap(clearTilemap.size.x, clearTilemap.size.y);

        ///////
        //(heightmap as Heightmap).TMP_SaveToTexture_REMOVE_LATER("heightmap.png");
        //////
        //return;

        Dictionary<ITilemapLevel, Range> levelHeights = PrepareHeightRanges(heightmap);

        Dictionary<ITileType, IReadOnlyList<IWorldBiome>> worldBiomes = new Dictionary<ITileType, IReadOnlyList<IWorldBiome>>();
        //TMP_PRINT_LEVELS(levelHeights);
        //return;

        bool[,] created = new bool[clearTilemap.size.y, clearTilemap.size.x];

        for (int i = 0; i < clearTilemap.size.y; ++i)
        {
            for (int j = 0; j < clearTilemap.size.x; ++j)
            {
                if (!created[i, j])
                {
                    int biomeSize = 0;
                    ITilemapLevel level = GetLevel(heightmap.GetHeightValue(j, i), levelHeights);
                    TileSet biome = GetTileset(heightmap.GetHeightValue(j, i), level);
                    int maxBiomeSize = maxBiomeSizes[biomes.IndexOf(biome)];
                    Range range = levelHeights[level];

                    Queue<int> lines = new Queue<int>();
                    Queue<int> columns = new Queue<int>();
                    lines.Enqueue(i);
                    columns.Enqueue(j);
                    created[i, j] = true;
                    
                    //bool biomeCreated = false;
                    if (!worldBiomes.ContainsKey(biome.Tiles[0].TileType))
                        worldBiomes.Add(biome.Tiles[0].TileType, new List<IWorldBiome>());
                    //if (!biomeCreated)
                    //{
                        ((List<IWorldBiome>)worldBiomes[biome.Tiles[0].TileType]).Add(new WorldBiome((TileType)biome.Tiles[0].TileType));
                        //biomeCreated = true;
                    //}
                    ((WorldBiome)worldBiomes[biome.Tiles[0].TileType][worldBiomes[biome.Tiles[0].TileType].Count - 1]).AddPosition(new Vector2Int(j, i));

                    while (lines.Count > 0)
                    {
                        int line = lines.Dequeue();
                        int column = columns.Dequeue();
                        biomeSize++;

                        var tile = biome.GetRandomTile();
                        clearTilemap.SetTile(new Vector3Int(column, line, 0), tile);

                        //if (tile.TileType.TileTypeName == "Dirt")
                        //    print("Oops!");

                        if (biomeSize >= maxBiomeSize)
                            break;

                        CheckAndAddNeighbor(heightmap, created, range, lines, columns, line - 1, column, (WorldBiome)worldBiomes[tile.TileType][worldBiomes[tile.TileType].Count - 1]);
                        CheckAndAddNeighbor(heightmap, created, range, lines, columns, line, column - 1, (WorldBiome)worldBiomes[tile.TileType][worldBiomes[tile.TileType].Count - 1]);
                        CheckAndAddNeighbor(heightmap, created, range, lines, columns, line + 1, column, (WorldBiome)worldBiomes[tile.TileType][worldBiomes[tile.TileType].Count - 1]);
                        CheckAndAddNeighbor(heightmap, created, range, lines, columns, line, column + 1, (WorldBiome)worldBiomes[tile.TileType][worldBiomes[tile.TileType].Count - 1]);
                    }

                    ClearCreated(created, lines, columns);
                }
            }
        }

        ((World)world).SetBiomes(worldBiomes);
    }

    private static void CheckAndAddNeighbor(IHeightmap heightmap, bool[,] created, Range range, Queue<int> lines, Queue<int> columns, int line, int column, WorldBiome worldBiome)
    {
        if (line >= 0 &&
            line < created.GetLength(0) &&
            column >= 0 &&
            column < created.GetLength(1) &&
            !created[line, column] &&
            heightmap.GetHeightValue(column, line) >= range.min &&
            heightmap.GetHeightValue(column, line) <= range.max)
        {
            lines.Enqueue(line);
            columns.Enqueue(column);
            created[line, column] = true;
            worldBiome.AddPosition(new Vector2Int(column, line));
        }
    }
    private void ClearCreated(bool[,] created, Queue<int> lines, Queue<int> columns)
    {
        while (lines.Count > 0)
        {
            int line = lines.Dequeue();
            int column = columns.Dequeue();
            created[line, column] = false;
        }
    }
    #endregion

    #region STACK
    //public void Generate(Tilemap clearTilemap)
    //{
    //    var heightmap = heightMapBuilder.BuildHeightMap(width, height);

    //    ///////
    //    //(heightmap as Heightmap).TMP_SaveToTexture_REMOVE_LATER("heightmap.png");
    //    //////
    //    //return;

    //    Dictionary<ITilemapLevel, Range> levelHeights = PrepareHeightRanges(heightmap);

    //    TMP_PRINT_LEVELS(levelHeights);
    //    //return;

    //    bool[,] created = new bool[height, width];

    //    for (int i = 0; i < height; ++i)
    //    {
    //        for (int j = 0; j < width; ++j)
    //        {
    //            if (!created[i, j])
    //            {
    //                int biomeSize = 0;
    //                ITilemapLevel level = GetLevel(heightmap.GetHeightValue(j, i), levelHeights);
    //                TileSet biome = GetTileset(heightmap.GetHeightValue(j, i), level);
    //                int maxBiomeSize = maxBiomeSizes[biomes.IndexOf(biome)];
    //                Range range = levelHeights[level];

    //                Stack<int> lines = new Stack<int>();
    //                Stack<int> columns = new Stack<int>();
    //                lines.Push(i);
    //                columns.Push(j);
    //                created[i, j] = true;
    //                while (lines.Count > 0)
    //                {
    //                    int line = lines.Pop();
    //                    int column = columns.Pop();
    //                    biomeSize++;

    //                    clearTilemap.SetTile(new Vector3Int(column, line, 0), biome.GetRandomTile());

    //                    if (biomeSize >= maxBiomeSize)
    //                        break;

    //                    CheckAndAddNeighbor(heightmap, created, range, lines, columns, line - 1, column);
    //                    CheckAndAddNeighbor(heightmap, created, range, lines, columns, line, column - 1);
    //                    CheckAndAddNeighbor(heightmap, created, range, lines, columns, line + 1, column);
    //                    CheckAndAddNeighbor(heightmap, created, range, lines, columns, line, column + 1);
    //                }

    //                ClearCreated(created, lines, columns);
    //            }
    //        }
    //    }
    //}

    //private static void CheckAndAddNeighbor(IHeightmap heightmap, bool[,] created, Range range, Stack<int> lines, Stack<int> columns, int line, int column)
    //{
    //    if (line >= 0 &&
    //        line < created.GetLength(0) &&
    //        column >= 0 &&
    //        column < created.GetLength(1) &&
    //        !created[line, column] &&
    //        heightmap.GetHeightValue(column, line) >= range.min &&
    //        heightmap.GetHeightValue(column, line) <= range.max)
    //    {
    //        lines.Push(line);
    //        columns.Push(column);
    //        created[line, column] = true;
    //    }
    //}
    //private void ClearCreated(bool[,] created, Stack<int> lines, Stack<int> columns)
    //{
    //    while (lines.Count > 0)
    //    {
    //        int line = lines.Pop();
    //        int column = columns.Pop();
    //        created[line, column] = false;
    //    }
    //}
    #endregion

    #region RANDOM LIST
    //public void Generate(Tilemap clearTilemap)
    //{
    //    var heightmap = heightMapBuilder.BuildHeightMap(width, height);

    //    ///////
    //    //(heightmap as Heightmap).TMP_SaveToTexture_REMOVE_LATER("heightmap.png");
    //    //////
    //    //return;

    //    Dictionary<ITilemapLevel, Range> levelHeights = PrepareHeightRanges(heightmap);

    //    TMP_PRINT_LEVELS(levelHeights);
    //    //return;

    //    bool[,] created = new bool[height, width];

    //    for (int i = 0; i < height; ++i)
    //    {
    //        for (int j = 0; j < width; ++j)
    //        {
    //            if (!created[i, j])
    //            {
    //                int biomeSize = 0;
    //                ITilemapLevel level = GetLevel(heightmap.GetHeightValue(j, i), levelHeights);
    //                TileSet biome = GetTileset(heightmap.GetHeightValue(j, i), level);
    //                int maxBiomeSize = maxBiomeSizes[biomes.IndexOf(biome)];
    //                Range range = levelHeights[level];

    //                List<int> linesList = new List<int>();
    //                List<int> columnsList = new List<int>();
    //                linesList.Add(i);
    //                columnsList.Add(j);
    //                created[i, j] = true;
    //                while (linesList.Count > 0)
    //                {
    //                    int index = Random.Range(0, linesList.Count);
    //                    int line = linesList[index];
    //                    int column = columnsList[index];
    //                    linesList.RemoveAt(index);
    //                    columnsList.RemoveAt(index);

    //                    biomeSize++;

    //                    clearTilemap.SetTile(new Vector3Int(column, line, 0), biome.GetRandomTile());

    //                    if (biomeSize >= maxBiomeSize)
    //                        break;

    //                    CheckAndAddNeighbor(heightmap, created, range, linesList, columnsList, line - 1, column);
    //                    CheckAndAddNeighbor(heightmap, created, range, linesList, columnsList, line, column - 1);
    //                    CheckAndAddNeighbor(heightmap, created, range, linesList, columnsList, line + 1, column);
    //                    CheckAndAddNeighbor(heightmap, created, range, linesList, columnsList, line, column + 1);
    //                }

    //                ClearCreated(created, linesList, columnsList);
    //            }
    //        }
    //    }
    //}

    //private static void CheckAndAddNeighbor(IHeightmap heightmap, bool[,] created, Range range, List<int> lines, List<int> columns, int line, int column)
    //{
    //    if (line >= 0 &&
    //        line < created.GetLength(0) &&
    //        column >= 0 &&
    //        column < created.GetLength(1) &&
    //        !created[line, column] &&
    //        heightmap.GetHeightValue(column, line) >= range.min &&
    //        heightmap.GetHeightValue(column, line) <= range.max)
    //    {
    //        lines.Add(line);
    //        columns.Add(column);
    //        created[line, column] = true;
    //    }
    //}
    //private void ClearCreated(bool[,] created, List<int> lines, List<int> columns)
    //{
    //    for(int i = 0; i < lines.Count; ++i)
    //    {
    //        created[lines[i], columns[i]] = false;
    //    }

    //    lines.Clear();
    //    columns.Clear();
    //}

    #endregion

    private Dictionary<ITilemapLevel, Range> PrepareHeightRanges(IHeightmap heightmap)
    {
        Dictionary<ITilemapLevel, Range> levelHeights = new Dictionary<ITilemapLevel, Range>();
        var hist = heightmap.GetHeightStatList(heightStatListStep);

        int curHeight = hist.MaxHeight;

        foreach (var level in tilemapSplitting.TilemapLevels)
        {
            float maxHeight = curHeight;
            float levelArea = 0;
            float barrier = tilemapSplitting.GetLevelPercentSize(level);
            while (levelArea < barrier && curHeight > heightmap.MinHeightValue)
            {
                levelArea += hist.GetStat(curHeight);
                curHeight -= hist.Step;
            }
            Range r = new Range();
            r.min = curHeight;
            r.max = maxHeight;
            levelHeights.Add(level, r);
        }

        return levelHeights;
    }

    private TileSet GetTileset(int height, ITilemapLevel level)
    {
        List<TileSet> sets = new List<TileSet>();
        foreach (var ts in biomes)
            if (ts.CanBePlacedOn(level))
                sets.Add(ts);

        if (sets.Count == 0)
            return null;

        return sets[Random.Range(0, sets.Count)];
    }

    private ITilemapLevel GetLevel(float height, Dictionary<ITilemapLevel, Range> levelHeights)
    {
        ITilemapLevel level = null;
        foreach (var lr in levelHeights)
            if (lr.Value.min <= height && lr.Value.max >= height)
                level = lr.Key;
        return level;
    }
}

public struct Range
{
    public float min;
    public float max;
}