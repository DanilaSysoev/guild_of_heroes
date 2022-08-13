using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile Type", menuName = "World/TileType")]
public class TileType : ScriptableObject, ITileType
{
    [SerializeField]
    private string tileTypeName;
    public string TileTypeName { get { return tileTypeName; } }    

    [SerializeField]
    private float passability;
    public float Passability { get { return passability; } }

    [SerializeField]
    private List<TileEffect> tileEffects;
    public IReadOnlyList<ITileEffect> TileEffects { get { return tileEffects; } }

    [SerializeField]
    private List<TileResource> potencialResources;
    public IReadOnlyList<ITileResource> PotencialResources { get { return potencialResources; } }

    [SerializeField]
    private List<float> potencialResourcesProbabilities;
    public IReadOnlyList<float> PotencialResourcesProbabilities { get { return potencialResourcesProbabilities; } }

}
