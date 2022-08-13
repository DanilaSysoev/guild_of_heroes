using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISettlementGenerationPrototype
{
    ISettlementType SettlementType { get; }
    
    IReadOnlyList<Sprite> SuitableSprites { get; }
    IReadOnlyList<Sprite> SuitableSatellitesSprites { get; }

    IReadOnlyList<IRace> MainRaces { get; }
    IReadOnlyList<float> MainRacesWeights { get; }
    int MinMainRacesCount { get; }
    int MaxMainRacesCount { get; }
    IReadOnlyList<IRace> SecondaryRaces { get; }
    IReadOnlyList<float> SecondaryRacesWeights { get; }
    int MaxTotalRacesCount { get; }

    IReadOnlyList<IClass> MainClasses { get; }
    IReadOnlyList<float> MainClassesWeights { get; }
    int MinMainClassesCount { get; }
    int MaxMainClassesCount { get; }
    IReadOnlyList<IClass> SecondaryClasses { get; }
    IReadOnlyList<float> SecondaryClassesWeights { get; }
    int MaxTotalClassesCount { get; }

    IReadOnlyList<ITileType> SuitableTileTypes { get; }
}
