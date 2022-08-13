using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tileset", menuName = "TileSet")]
public class TileSet : ScriptableObject, ITileSet
{
    [SerializeField]
    private List<WorldTile> tiles;
    public IReadOnlyList<WorldTile> Tiles { get { return tiles; } }

    [SerializeField]
    private List<TilemapLevel> suitableLevels;
    public IReadOnlyList<ITilemapLevel> SuitableLevels { get { return suitableLevels; } }
    
    public bool CanBePlacedOn(ITilemapLevel level)
    {
        return suitableLevels.Contains((TilemapLevel)level);
    }

    public WorldTile GetRandomTile()
    {
        var tile = tiles[Random.Range(0, tiles.Count)];
        return tile;
    }
}
