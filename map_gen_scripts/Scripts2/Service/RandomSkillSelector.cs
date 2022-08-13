using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Атрибут настройки выбора случайного значения скила по-умолчанию
/// </summary>
[Serializable]
public class SkillGenerationAttributes
{
    [SerializeField]
    private Skill skill;

    [SerializeField]
    [Min(0)]
    private int minDefaultValue;

    [SerializeField]
    [Min(0)]
    private int maxDefaultValue;

    [SerializeField]
    private FromRangeRandomSelector rangeSelector;

    /// <summary>
    /// Скил
    /// </summary>
    public Skill                    Skill           { get { return skill; } }
    /// <summary>
    /// Минимальное значение по умолчанию
    /// </summary>
    public int                      MinDefaultValue { get { return minDefaultValue; } }
    /// <summary>
    /// Максимальное значение скила по-умолчанию
    /// </summary>
    public int                      MaxDefaultValue { get { return maxDefaultValue; } }
    /// <summary>
    /// Объект выбора из случайного значения из диапазона
    /// </summary>
    public FromRangeRandomSelector  RangeSelector   { get { return rangeSelector; } }

    /// <summary>
    /// Метод для автоматической установки. Вызывается генератором.
    /// </summary>
    /// <param name="skill">Скил</param>
    /// <param name="rangeSelector">Объект выбора из случайного значения из диапазона</param>
    /// <param name="minValue">Минимальное значение по умолчанию</param>
    /// <param name="maxValue">Максимальное значение скила по-умолчанию</param>
    public void Set(Skill skill, FromRangeRandomSelector rangeSelector, int minValue = 0, int maxValue = 3)
    {
        this.skill = skill;
        this.rangeSelector = rangeSelector;
        minDefaultValue = minValue;
        maxDefaultValue = maxValue;
    }
}

/// <summary>
/// Класс выбора случайных значений скилов по-умолчанию
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "newSkillsSelector", menuName = "New/Skills selector")]
public class RandomSkillSelector : ScriptableObject
{
    /// <summary>
    /// Список атрибутов выбора случайных значений
    /// </summary>
    [SerializeField]    
    private List<SkillGenerationAttributes> skillAttributes;

    /// <summary>
    /// Реализучет простой выбор для списка из диапазона, 
    /// с использованием объекта-селектора
    /// </summary>
    /// <returns></returns>
    public Dictionary<Skill, int> GetSkillsWithDefaultValues()
    {
        return RandomSelectorsService.GetEntitiesWithValue<Skill>(
            skillAttributes.Select(sa => sa.Skill).ToList(),
            skillAttributes.Select(sa => sa.MinDefaultValue).ToList(),
            skillAttributes.Select(sa => sa.MaxDefaultValue).ToList(),
            skillAttributes.Select(sa => sa.RangeSelector).ToList());
    }

    /// <summary>
    /// Метод для Editor
    /// Для всех скилов в базе создает атрибуты с min-max по-умолчанию
    /// и обычным Uniform-селектором из диапазона
    /// </summary>
    [ContextMenu("Set Default")]
    private void LoadDefaultValues()
    {
        var skills = AssetDatabase.FindAssets("t: Skill");
        var rangeSelector = AssetDatabase.LoadAssetAtPath<FromRangeRandomSelector>(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("UniformRangeSelector")[0]));

        skillAttributes = new List<SkillGenerationAttributes>();

        foreach(var s in skills)
        {
            var path = AssetDatabase.GUIDToAssetPath(s);
            var skill = AssetDatabase.LoadAssetAtPath<Skill>(path);
            var sga = new SkillGenerationAttributes();

            sga.Set(skill, rangeSelector);
            skillAttributes.Add(sga);
        }
    }
}
