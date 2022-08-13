using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile Resource", menuName = "World/TileResource")]
public class TileResource : ScriptableObject, ITileResource
{
    [SerializeField]
    private string resourceName;
    public string ResourceName { get { return resourceName; } }

    [SerializeField]
    private Sprite sprite;
    public Sprite Sprite { get { return sprite; } }
}
