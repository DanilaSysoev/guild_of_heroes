using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITileType
{
    string TileTypeName { get; }
    float Passability { get; }

    IReadOnlyList<ITileEffect> TileEffects { get; }

    IReadOnlyList<ITileResource> PotencialResources { get; }
    IReadOnlyList<float> PotencialResourcesProbabilities { get; }
}
