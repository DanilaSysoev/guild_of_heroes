using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Компонент случайного выбора спрайта
/// </summary>
[Serializable]
public class SpriteGenerationAttribute
{
    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    [Min(0)]
    private int spriteWeight;
    
    [SerializeField]
    private Race race;

    [SerializeField]
    private PersonClass personClass;

    /// <summary>
    /// Спрайт
    /// </summary>
    public Sprite       Sprite          { get { return sprite; } }
    /// <summary>
    /// Раса спрайта
    /// </summary>
    public Race         Race            { get { return race; } }
    /// <summary>
    /// Класс спрайта
    /// </summary>
    public PersonClass  PersonClass     { get { return personClass; } }
    /// <summary>
    /// Вес спрайта
    /// </summary>
    public int          SpriteWeight    { get { return spriteWeight; } }
    
    /// <summary>
    /// Метод для автоматической установки. Вызывается генератором.
    /// </summary>
    /// <param name="race">Раса</param>
    /// <param name="personClass">Класс</param>
    /// <param name="sprite">Спрайт</param>
    /// <param name="weight">Вес спрайта</param>
    public void Set(Race race, PersonClass personClass, Sprite sprite, int weight = 1)
    {
        this.race = race;
        this.personClass = personClass;
        this.sprite = sprite;
        spriteWeight = weight;
    }
}

/// <summary>
/// Скриптовый объект случайного выбора спрайта для персонажа.
/// Используется в генераторе наемников.
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "newSpriteSelector", menuName = "New/Sprite selector")]
public class RandomSpriteSelector : ScriptableObject
{
    /// <summary>
    /// Список компонентов для выбора
    /// </summary>
    [SerializeField]
    private List<SpriteGenerationAttribute> spriteAttributes = new List<SpriteGenerationAttribute>();

    /// <summary>
    /// Сгруппированный по расам и классам список компонентов
    /// </summary>
    private Dictionary<Race, Dictionary<PersonClass, List<SpriteGenerationAttribute>>> spriteAttributesDict;

    private void SetupDict()
    {
        // Группировка по расам и классам
        ///////////////////////////////////
        spriteAttributesDict = new Dictionary<Race, Dictionary<PersonClass, List<SpriteGenerationAttribute>>>();
        foreach (var sa in spriteAttributes)
        {
            if (!spriteAttributesDict.ContainsKey(sa.Race))
                spriteAttributesDict.Add(sa.Race, new Dictionary<PersonClass, List<SpriteGenerationAttribute>>());
            if (!spriteAttributesDict[sa.Race].ContainsKey(sa.PersonClass))
                spriteAttributesDict[sa.Race].Add(sa.PersonClass, new List<SpriteGenerationAttribute>());
            spriteAttributesDict[sa.Race][sa.PersonClass].Add(sa);
        }
        ///////////////////////////////////
    }

    /// <summary>
    /// Возвращает случайный спрайт для данных расы и класса
    /// </summary>
    /// <param name="race">Раса</param>
    /// <param name="personClass">Класс</param>
    /// <returns>Спрайт</returns>
    public Sprite GetRandomSprite(Race race, PersonClass personClass)
    {
        // Проверяем, производилась ли группировка
        ////////////////////////////////////////////
        if (spriteAttributesDict == null)
            SetupDict();
        ////////////////////////////////////////////

        // С помощью случайного взвешенного выбора выбираем спрайт, используя веса компоненьтов выбора
        ////////////////////////////////////////////////////////////////////////////////////////////////
        return RandomSelectorsService.GetEntityByWeights<Sprite>(
            spriteAttributesDict[race][personClass].Select(sa => sa.Sprite).ToList(),
            spriteAttributesDict[race][personClass].Select(sa => sa.SpriteWeight).ToList());
        ////////////////////////////////////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// Метод для Editor
    /// Создает для всех рас и классов атрибуты со спрайтами, 
    /// используя расы как имена спрайтов.
    /// </summary>
    [ContextMenu("Set Default")]
    private void LoadDefaultValues()
    {
        var races = AssetDatabase.FindAssets("t: Race");
        var classes = AssetDatabase.FindAssets("t: PersonClass");
        var spriteGUIDs = AssetDatabase.FindAssets("t: Sprite");
        List<Sprite> sprites = new List<Sprite>();
        foreach (var s in spriteGUIDs)
            sprites.Add(AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GUIDToAssetPath(s)));

        spriteAttributes = new List<SpriteGenerationAttribute>();

        foreach (var r in races)
        {
            var race = AssetDatabase.LoadAssetAtPath<Race>(AssetDatabase.GUIDToAssetPath(r));
            var sprite = sprites.Find(s => s.name == race.RaceName);

            foreach(var c in classes)
            {
                var pClass = AssetDatabase.LoadAssetAtPath<PersonClass>(AssetDatabase.GUIDToAssetPath(c));
                SpriteGenerationAttribute sga = new SpriteGenerationAttribute();
                sga.Set(race, pClass, sprite);
                spriteAttributes.Add(sga);
            }
        }

        SetupDict();
    }
}
