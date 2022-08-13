using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHeightmapBuilder
{
    IHeightmap BuildHeightMap(int sizeX, int sizeY);
}
