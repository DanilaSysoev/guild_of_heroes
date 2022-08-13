using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SettlementPrototype", menuName = "World/SettlementPrototype")]
public class SettlementGenerationPrototype : ScriptableObject, ISettlementGenerationPrototype
{
    [SerializeField]
    private SettlementType settlementType;
    public ISettlementType SettlementType { get { return settlementType; } }

    [SerializeField]
    private List<Sprite> suitableSprites;
    public IReadOnlyList<Sprite> SuitableSprites { get { return suitableSprites; } }

    [SerializeField]
    private List<Sprite> suitableSatellitesSprites;
    public IReadOnlyList<Sprite> SuitableSatellitesSprites { get { return suitableSatellitesSprites; } }

    [SerializeField]
    private List<Race> mainRaces;
    public IReadOnlyList<IRace> MainRaces { get { return mainRaces; } }
    [SerializeField]
    private List<float> mainRacesWeights;
    public IReadOnlyList<float> MainRacesWeights { get { return mainRacesWeights; } }
    [SerializeField]
    private int minMainRacesCount;
    public int MinMainRacesCount { get { return minMainRacesCount; } }
    [SerializeField]
    private int maxMainRacesCount;
    public int MaxMainRacesCount { get { return maxMainRacesCount; } }
    [SerializeField]
    private List<Race> secondaryRaces;
    public IReadOnlyList<IRace> SecondaryRaces { get { return secondaryRaces; } }
    [SerializeField]
    private List<float> secondaryRacesWeights;
    public IReadOnlyList<float> SecondaryRacesWeights { get { return secondaryRacesWeights; } }
    [SerializeField]
    private int maxTotalRacesCount;
    public int MaxTotalRacesCount { get { return maxTotalRacesCount; } }

    [SerializeField]
    private List<Class> mainClasses;
    public IReadOnlyList<IClass> MainClasses { get { return mainClasses; } }
    [SerializeField]
    private List<float> mainClassesWeights;
    public IReadOnlyList<float> MainClassesWeights { get { return mainClassesWeights; } }
    [SerializeField]
    private int minMainClassesCount;
    public int MinMainClassesCount { get { return minMainClassesCount; } }
    [SerializeField]
    private int maxMainClassesCount;
    public int MaxMainClassesCount { get { return maxMainClassesCount; } }
    [SerializeField]
    private List<Class> secondaryClasses;
    public IReadOnlyList<IClass> SecondaryClasses { get { return secondaryClasses; } }
    [SerializeField]
    private List<float> secondaryClassesWeights;
    public IReadOnlyList<float> SecondaryClassesWeights { get { return secondaryClassesWeights; } }
    [SerializeField]
    private int maxTotalClassesCount;
    public int MaxTotalClassesCount { get { return maxTotalClassesCount; } }

    [SerializeField]
    private List<TileType> suitableTileTypes;
    public IReadOnlyList<ITileType> SuitableTileTypes { get { return suitableTileTypes; } }
}
