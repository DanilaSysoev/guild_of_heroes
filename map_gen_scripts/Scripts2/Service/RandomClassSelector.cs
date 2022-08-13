using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Инкапсулирует вес класса при генерации
/// </summary>
[Serializable]
public class ClassGenerationWeight
{
    [SerializeField]
    private PersonClass personClass;

    [SerializeField]
    [Min(0)]
    private int classWeight;

    /// <summary>
    /// Класс
    /// </summary>
    public PersonClass Class { get { return personClass; } set { personClass = value; } }
    /// <summary>
    /// Вес класса
    /// </summary>
    public int ClassWeight { get { return classWeight; } set { classWeight = value; } }
}

/// <summary>
/// Инкапсулирует атрибут генерации класса.
/// Учитывает расу. У разных рас разные веса классов
/// </summary>
[Serializable]
public class ClassGenerationAttribute
{
    [SerializeField]
    private Race race;
    /// <summary>
    /// Раса
    /// </summary>
    public Race Race { get { return race; } }

    [SerializeField]
    private List<ClassGenerationWeight> classWeights;
    /// <summary>
    /// Коллекция классов с вечами для данной расы
    /// </summary>
    public ICollection<ClassGenerationWeight> ClassWeights { get { return classWeights; } }

    /// <summary>
    /// Метод для автоматической установки. Вызывается генератором.
    /// </summary>
    /// <param name="race">Раса</param>
    /// <param name="classes">Список классов</param>
    /// <param name="weights">Список весов</param>
    public void Set(Race race, List<PersonClass> classes, List<int> weights)
    {
        this.race = race;
        classWeights = new List<ClassGenerationWeight>();
        for (int i = 0; i < classes.Count; ++i)
            classWeights.Add(new ClassGenerationWeight { Class = classes[i], ClassWeight = weights[i] });
    }
}

/// <summary>
/// Объект случайного выбора класса наемника
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "newClassSelector", menuName = "New/Class selector")]
public class RandomClassSelector : ScriptableObject
{
    /// <summary>
    /// Значение веса класса по умолчанию.
    /// Используется при автоматическом создании атрибутов генерации.
    /// </summary>
    [SerializeField]
    [Tooltip("Значение веса класса по умолчанию.\n" +
             "Используется при автоматическом создании атрибутов генерации.")]
    private int defaultClassWeight = 100;
    /// <summary>
    /// Список атрибутов генерации
    /// </summary>
    [SerializeField]
    private List<ClassGenerationAttribute> classAttributes;

    /// <summary>
    /// Возвращает случайный класс для указанной расы
    /// </summary>
    /// <param name="race">Раса</param>
    /// <returns>Случайный класс</returns>
    public PersonClass GetRandomClass(Race race)
    {
        ClassGenerationAttribute cga = classAttributes.Find(a => a.Race == race);

        return RandomSelectorsService.GetEntityByWeights<PersonClass>(
            cga.ClassWeights.Select(ca => ca.Class).ToList(),
            cga.ClassWeights.Select(ca => ca.ClassWeight).ToList());
    }

    /// <summary>
    /// Метода для Editor
    /// Создает для всех рас и классов атрибуты генерации
    /// с весами классов по умолчанию.
    /// ПРОЩЕ СОЗДАТЬ ДЛЯ ВСЕХ И НАСТРОИТЬ, УДАЛИВ ЛИШНЕЕ,
    /// ЧЕМ ПРОПИСЫВАТЬ ВРУЧНУЮ ВСЕ АТРИБУТЫ.
    /// </summary>
    [ContextMenu("BuildDefault")]
    public void BuildDefaultSelector()
    {
        classAttributes = new List<ClassGenerationAttribute>();

        var racesGUIDs = AssetDatabase.FindAssets("t: Race");
        var classesGUIDs = AssetDatabase.FindAssets("t: PersonClass");
        List<PersonClass> classes = new List<PersonClass>();
        foreach (var cid in classesGUIDs)
            classes.Add(AssetDatabase.LoadAssetAtPath<PersonClass>(AssetDatabase.GUIDToAssetPath(cid)));

        List<int> weights = new List<int>();
        for (int i = 0; i < classes.Count; ++i)
            weights.Add(defaultClassWeight);


        foreach (var rgid in racesGUIDs)
        {
            var race = AssetDatabase.LoadAssetAtPath<Race>(AssetDatabase.GUIDToAssetPath(rgid));
            var cga = new ClassGenerationAttribute();
            cga.Set(race, classes, weights);
            classAttributes.Add(cga);
        }
    }
}
