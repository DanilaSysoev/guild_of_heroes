using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITilemapLevel
{
    string LevelName { get; }
    int RelativeHeight { get; }
}
