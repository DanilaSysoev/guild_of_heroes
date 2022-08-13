using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualServices : MonoBehaviour, IVisualServices
{
    public static IVisualServices Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }

    public void HideResourceIcons(IWorld world)
    {
        var visualizers = world.Tilemap.GetComponentsInChildren<ResourceVisualizer>();
        foreach (var v in visualizers)
            v.HideResource();
    }

    public void ShowResourceIcons(IWorld world)
    {
        var visualizers = world.Tilemap.GetComponentsInChildren<ResourceVisualizer>();
        foreach (var v in visualizers)
            v.ShowResource();
    }
}
