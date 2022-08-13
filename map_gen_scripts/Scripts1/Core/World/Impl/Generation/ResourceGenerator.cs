using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ResourceGenerator : MonoBehaviour, IWorldGenerator
{
    [SerializeField]
    private GameObject resourceGameObject;

    public void Generate(IWorld world)
    {
        var tilemap = world.Tilemap;

        for(int x = 0; x < world.SizeX; ++x)
        {
            for (int y = 0; y < world.SizeY; ++y)
            {
                WorldTile tile = (WorldTile)tilemap.GetTile(new Vector3Int(x, y, 0));
                IWorldTileInfo tileInfo = world.GetTileInfo(new Vector2Int(x, y));
                tileInfo.Resource = Randomizer.GetItemFromRow<ITileResource>(tile.TileType.PotencialResources, tile.TileType.PotencialResourcesProbabilities);

                var resGO = Instantiate(resourceGameObject, tilemap.GetInstantiatedObject(new Vector3Int(x, y, 0)).transform);

                resGO.GetComponent<ResourceVisualizer>().SetResource(tileInfo.Resource);
            }
        }
    }
}
