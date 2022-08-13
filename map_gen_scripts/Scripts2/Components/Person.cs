using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Наемника (герой)
/// </summary>
public class Person : MonoBehaviour
{
    /// <summary>
    /// Раса наемника
    /// </summary>
    public Race                     Race    { get; private set; }
    /// <summary>
    /// Класс наемника
    /// </summary>
    public PersonClass              Class   { get; private set; }
    /// <summary>
    /// Спрайт наемника
    /// </summary>
    public Sprite                   Sprite  { get; private set; }
    /// <summary>
    /// Список скиллов и значений наемника
    /// </summary>
    public Dictionary<Skill, int>   Skills  { get; private set; }

    /// <summary>
    /// Создание наемника
    /// </summary>
    /// <param name="race">Раса</param>
    /// <param name="personClass">Класс</param>
    /// <param name="sprite">Спрайт</param>
    /// <param name="skills">Скиллы и значения</param>
    public void Init(Race race, PersonClass personClass, Sprite sprite, Dictionary<Skill, int> skills)
    {
        Race = race;
        Class = personClass;
        Sprite = sprite;
        Skills = new Dictionary<Skill, int>(skills);
    }
}
