using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TilemapGeneratorTest : MonoBehaviour
{
    [SerializeField]
    Tilemap tilemap;
    [SerializeField]
    WorldGenerator worldGenerator;
    [SerializeField]
    Camera Camera;

    Vector2Int? start = null;
    
    IPath pastPath = new Path();

    bool pathEnabled = false;
    public void PathEnable(Toggle toggle)
    {
        pathEnabled = toggle.isOn;
    }

    private void Awake()
    {
        Random.InitState(0);
    }

    bool tested = false;
    private void Update()
    {
        //if(!tested && (World.Instance as World).biomes != null)
        //{
        //    foreach (var tt in World.Instance.GetAllTileTypes())
        //        foreach (var biome in World.Instance.GetBiomes(tt))
        //            for (int i = 0; i < biome.TilesPositions.Count; ++i)
        //                for (int j = 0; j < biome.TilesPositions.Count; ++j)
        //                    if (i != j && biome.TilesPositions[i] == biome.TilesPositions[j])
        //                        print("DUPLICATE_TILES!");

        //    tested = true;
        //}

        if (pathEnabled)
        {
            if (Input.GetMouseButtonDown(1))
            {
                start = null;
                ClearColors();
            }
            if (Input.GetMouseButtonDown(0))
            {
                var t = tilemap.WorldToCell(Camera.ScreenToWorldPoint(Input.mousePosition));
                start = new Vector2Int(t.x, t.y);

                ClearColors();
            }

            if (Input.GetKeyDown(KeyCode.Q))
                print("");

            if (start != null)
            {
                ClearColors();

                var t = tilemap.WorldToCell(Camera.ScreenToWorldPoint(Input.mousePosition));

                if (t.x >= 0 && t.y >= 0 && t.x < tilemap.size.x && t.y < tilemap.size.y)
                {
                    pastPath = World.Instance.GetPath(start.Value, new Vector2Int(t.x, t.y));
                    if (pastPath != null)
                    {
                        for (int i = 0; i < pastPath.Points.Count; ++i)
                            World.Instance.Tilemap.SetColor(new Vector3Int(pastPath.Points[i].x, pastPath.Points[i].y, 0), Color.red);
                    }
                }
            }
        }
    }

    private void ClearColors()
    {
        if (pastPath != null)
        {
            foreach (var p in pastPath.Points)
            {
                World.Instance.Tilemap.SetColor(new Vector3Int(p.x, p.y, 0), Color.white);
            }
        }
        pastPath = new Path();
    }

    public void Generate()
    {
        worldGenerator.Generate(World.Instance);
    }

    public void ResourceIcons(Toggle toggle)
    {
        if (toggle.isOn)
            VisualServices.Instance.ShowResourceIcons(World.Instance);
        else
            VisualServices.Instance.HideResourceIcons(World.Instance);
    }

    private List<Color> colors;
    public void SwitchBiomesVisualize(Toggle toggle)
    {
        if(colors == null)
        {
            colors = new List<Color>();
            foreach (var tt in World.Instance.GetAllTileTypes())
                foreach (var b in World.Instance.GetBiomes(tt))
                    colors.Add(new Color(
                        Random.Range(0.0f, 1.0f),
                        Random.Range(0.0f, 1.0f),
                        Random.Range(0.0f, 1.0f)));
        }
        if(toggle.isOn)
        {
            int i = 0;
            foreach (var tt in World.Instance.GetAllTileTypes())
                foreach (var b in World.Instance.GetBiomes(tt))
                {
                    foreach (var p in b.TilesPositions)
                        World.Instance.Tilemap.SetColor(new Vector3Int(p.x, p.y, 0), colors[i]);
                    ++i;
                }
        }
        else
        {
            int i = 0;
            foreach (var tt in World.Instance.GetAllTileTypes())
                foreach (var b in World.Instance.GetBiomes(tt))
                    foreach (var p in b.TilesPositions)
                        World.Instance.Tilemap.SetColor(new Vector3Int(p.x, p.y, 0), Color.white);
        }
    }
}
