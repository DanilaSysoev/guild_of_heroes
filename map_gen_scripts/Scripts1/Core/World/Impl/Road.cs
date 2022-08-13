using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour, IRoad
{
    private HashSet<IDirection> directions = new HashSet<IDirection>();
    public IEnumerable<IDirection> Directions { get { return directions; } }

    public void AddDirection(IDirection direction)
    {
        directions.Add(direction);
    }

    public int GetMask()
    {
        int mask = 0;
        foreach (var d in directions)
            mask |= d.GetBitMask();
        return mask;
    }
}
