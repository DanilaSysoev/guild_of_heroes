using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Атрибут случайного выбора расы наемника
/// </summary>
[Serializable]
public class RaceGenerationAttribute
{
    [SerializeField]
    private Race race;

    [SerializeField]
    [Min(0)]
    private int raceWeight;

    /// <summary>
    /// Раса
    /// </summary>
    public Race Race { get { return race; } }
    /// <summary>
    /// Вес расы
    /// </summary>
    public int RaceWeight { get { return raceWeight; } }
}

[Serializable]
[CreateAssetMenu(fileName = "newRaceSelector", menuName = "New/Race selector")]
public class RandomRaceSelector : ScriptableObject
{
    /// <summary>
    /// Список атрибутов выбора расы
    /// </summary>
    [SerializeField]
    private List<RaceGenerationAttribute> raceAttributes;

    /// <summary>
    /// Выбор расы с использованием механизма выбора случайного взвешенного
    /// </summary>
    /// <returns>Раса</returns>
    public Race GetRandomRace()
    {
        return RandomSelectorsService.GetEntityByWeights<Race>(
            raceAttributes.Select(ra => ra.Race).ToList(),
            raceAttributes.Select(ra => ra.RaceWeight).ToList());
    }
}