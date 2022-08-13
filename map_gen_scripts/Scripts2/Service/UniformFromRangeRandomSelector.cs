using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Реализует выбор случайного из диапазона по-умолчанию (Random-класс Unity)
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "newUniformSelector", menuName = "New/Uniform selector")]
public class UniformFromRangeRandomSelector : FromRangeRandomSelector
{
    /// <summary>
    /// Возвращает случайное из диапазона
    /// </summary>
    /// <param name="minModValue">Минимальное значение (включительно)</param>
    /// <param name="maxModValue">Максимальное значение (исключая)</param>
    /// <returns>Результат</returns>
    public override int CalculateValue(int minModValue, int maxModValue)
    {
        return UnityEngine.Random.Range(minModValue, maxModValue);
    }
}
