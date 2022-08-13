using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHeightmap
{
    int SizeX { get; }
    int SizeY { get; }
    
    int MinHeightValue { get; }
    int MaxHeightValue { get; }

    IHeightStatList GetHeightStatList(int step);
    int GetHeightValue(int x, int y);

    IHeightmap Clone();
}
