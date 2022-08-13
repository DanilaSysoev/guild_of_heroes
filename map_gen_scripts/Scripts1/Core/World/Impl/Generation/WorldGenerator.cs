using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour, IWorldGenerator
{
    [SerializeField]
    private int sizeX = 64;
    [SerializeField]
    private int sizeY = 48;

    [SerializeField]
    private TilemapGenerator tilemapGenerator;
    [SerializeField]
    private ResourceGenerator resourceGenerator;
    [SerializeField]
    private SettlementsGenerator settlementsGenerator;
    [SerializeField]
    private RoadsGenerator roadsGenerator;

    public void Generate(IWorld world)
    {
        world.SetSize(sizeX, sizeY);
        tilemapGenerator.Generate(world);
        resourceGenerator.Generate(world);
        settlementsGenerator.Generate(world);
        roadsGenerator.Generate(world);
    }
}
