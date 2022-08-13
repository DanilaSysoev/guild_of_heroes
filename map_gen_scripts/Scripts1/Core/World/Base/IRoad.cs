using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRoad
{
    IEnumerable<IDirection> Directions { get; }
    void AddDirection(IDirection direction);
    int GetMask();
}
