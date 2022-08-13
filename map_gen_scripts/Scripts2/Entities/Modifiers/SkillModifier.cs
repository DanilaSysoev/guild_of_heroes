using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Скриптовый объект для модификатора скила
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "skillModifier", menuName = "New/Skill modifier")]
public class SkillModifier : ScriptableObject
{
    [SerializeField]
    private Skill skill;
    /// <summary>
    /// Скил
    /// </summary>
    public Skill Skill { get { return skill; } }

    [SerializeField]
    private int minModifierValue;
    /// <summary>
    /// Минимальное значение модификатора
    /// </summary>
    public int MinModifierValue { get { return minModifierValue; } }

    [SerializeField]
    private int maxModifierValue;
    /// <summary>
    /// Максимальное значение модификатора
    /// </summary>
    public int MaxModifierValue { get { return maxModifierValue; } }

    /// <summary>
    /// Объект выбора случайного числа из диапазона
    /// Регулирует распределение
    /// </summary>
    [SerializeField]
    private FromRangeRandomSelector rangeSelector;

    /// <summary>
    /// Проверяет, является ли модификатор положительным
    /// </summary>
    public bool IsPositive { get { return maxModifierValue > 0; } }

    /// <summary>
    /// Возвращает следующее случайное значение модификатора
    /// </summary>
    /// <returns>Значение модификатора</returns>
    public int GetNextModifierValue()
    {
        return rangeSelector.CalculateValue(minModifierValue, maxModifierValue);
    }

    /// <summary>
    /// Используется для автоматического создание модификатора
    /// </summary>
    /// <param name="skill">Скил</param>
    /// <param name="minModValue">Минимальное значение модификатора</param>
    /// <param name="maxModValue">Максимальное значение модификатора</param>
    /// <param name="rangeSelector">Объект выбора из диапазона</param>
    public void Set(Skill skill, int minModValue, int maxModValue, FromRangeRandomSelector rangeSelector)
    {
        this.skill = skill;
        minModifierValue = minModValue;
        maxModifierValue = maxModValue;
        this.rangeSelector = rangeSelector;
    }
}