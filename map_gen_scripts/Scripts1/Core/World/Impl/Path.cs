using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : IPath
{
    private List<Vector2Int> points;
    public IReadOnlyList<Vector2Int> Points { get { return points; } }

    public Path()
    {
        points = new List<Vector2Int>();
    }
    public Path(List<Vector2Int> points)
    {
        this.points = points;
    }

    public void AddPoint(Vector2Int p)
    {
        points.Add(p);
    }
}
