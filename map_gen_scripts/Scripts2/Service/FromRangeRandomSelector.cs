using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Базовый абстрактный клас для селектора случайного из диапазона
/// </summary>
[Serializable]
public abstract class FromRangeRandomSelector : ScriptableObject
{
    /// <summary>
    /// Возвращает случайное из диапазона
    /// </summary>
    /// <param name="minModValue">Минимальное значение (включительно)</param>
    /// <param name="maxModValue">Максимальное значение (исключая)</param>
    /// <returns>Результат</returns>
    public abstract int CalculateValue(int minModValue, int maxModValue);
}