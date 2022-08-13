using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPath
{
    IReadOnlyList<Vector2Int> Points { get; }
}
