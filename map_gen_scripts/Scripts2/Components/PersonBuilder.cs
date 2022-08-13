using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Инкапсулирует расовые модификаторы скила для генератора наемников
/// </summary>
[Serializable]
public class RaceSkillModificationAttribute
{
    [SerializeField]
    private Race race;
    /// <summary>
    /// Раса
    /// </summary>
    public Race Race { get { return race; } }

    [SerializeField]
    private List<SkillModifier> skillModifiers;
    /// <summary>
    /// Модификаторы скиллов расы
    /// </summary>
    public ICollection<SkillModifier> SkillModifiers { get { return skillModifiers; } }

    /// <summary>
    /// Задание значений для модификаторов
    /// </summary>
    /// <param name="race">Раса</param>
    /// <param name="skillModifiers">Модификаторы</param>
    public void Set(Race race, List<SkillModifier> skillModifiers)
    {
        this.race = race;
        this.skillModifiers = new List<SkillModifier>(skillModifiers);
    }
}

/// <summary>
/// Генератор наемников
/// </summary>
public class PersonBuilder : MonoBehaviour
{
    /// <summary>
    /// Расовые модификаторы скиллов
    /// </summary>
    [SerializeField]
    private List<RaceSkillModificationAttribute> raceSkillModifiers;
    /// <summary>
    /// Правила классовых модификаций
    /// </summary>
    [SerializeField]
    private List<ClassSkillModificationRule> classSkillModificationRules;

    /// <summary>
    /// Селектор класса
    /// </summary>
    [SerializeField]
    private RandomClassSelector classSelector;
    /// <summary>
    /// Селектор расы
    /// </summary>
    [SerializeField]
    private RandomRaceSelector raceSelector;
    /// <summary>
    /// Селектор скиллов
    /// </summary>
    [SerializeField]
    private RandomSkillSelector skillSelector;
    /// <summary>
    /// Селектор спрайта
    /// </summary>
    [SerializeField]
    private RandomSpriteSelector spriteSelector;

    /// <summary>
    /// Префаб игрового объекта персонажа
    /// </summary>
    [SerializeField]
    private GameObject personPrefab;

    //Служебное поле для фасовки расовых модификаторов по расам
    private Dictionary<Race, RaceSkillModificationAttribute> raceSkillModifiersDict;
    //Служебное поле для фасовки классовых правил модификации скиллов по классам
    private Dictionary<PersonClass, List<ClassSkillModificationRule>> classSkillModificationRulesDict;

    private void Awake()
    {
        UnityEngine.Random.InitState(1);

        Init();
    }

    public void Init()
    {
        // Фасовка модификаторов
        //////////////////////////
        raceSkillModifiersDict = new Dictionary<Race, RaceSkillModificationAttribute>();
        foreach (var rsm in raceSkillModifiers)
            raceSkillModifiersDict[rsm.Race] = rsm;

        classSkillModificationRulesDict = new Dictionary<PersonClass, List<ClassSkillModificationRule>>();
        foreach (var csm in classSkillModificationRules)
        {
            if (!classSkillModificationRulesDict.ContainsKey(csm.Class))
                classSkillModificationRulesDict.Add(csm.Class, new List<ClassSkillModificationRule>());
            classSkillModificationRulesDict[csm.Class].Add(csm);
        }
        //////////////////////////
    }

    /// <summary>
    /// Создание наемника
    /// </summary>
    /// <returns>игрофой объект с настроенным наемником</returns>
    public GameObject BuildPerson()
    {
        // Выбор стартовых атрибутов
        //////////////////////////////
        var race = raceSelector.GetRandomRace();
        var pClass = classSelector.GetRandomClass(race);
        var skills = skillSelector.GetSkillsWithDefaultValues();        
        var sprite = spriteSelector.GetRandomSprite(race, pClass);
        //////////////////////////////

        // Применение расовых модификаторов 
        /////////////////////////////////////
        var rMod = raceSkillModifiersDict.ContainsKey(race) ? raceSkillModifiersDict[race] : null;
        if(rMod != null)
        {            
            foreach (var m in rMod.SkillModifiers)
                if (skills.ContainsKey(m.Skill))
                    skills[m.Skill] += m.GetNextModifierValue();
        }
        /////////////////////////////////////
        
        // Применение классовых модификаторов 
        ///////////////////////////////////////
        var cMods = classSkillModificationRulesDict.ContainsKey(pClass) ? classSkillModificationRulesDict[pClass] : new List<ClassSkillModificationRule>();
        foreach (var cm in cMods)
        {
            var mods = cm.GetModifiers(rMod);
            foreach (var m in mods)
                if (skills.ContainsKey(m.Key))
                    skills[m.Key] += m.Value;
        }
        ///////////////////////////////////////

        // Создание и настройка GO
        ////////////////////////////
        var person = Instantiate(personPrefab);
        person.GetComponent<Person>().Init(race, pClass, sprite, skills);
        ////////////////////////////

        return person;
    }

    /// <summary>
    /// Метод для Editor.
    /// Выбирает все расы из ассетов и все модификаторы этих рас
    /// (по префиксу в названии) и создает для них
    /// RaceSkillModificationAttribute со значениями по-уполчанию
    /// </summary>
    [ContextMenu("Fill Default Race Modifiers")]
    public void FillDefaultRaceModifiers()
    {
        raceSkillModifiers = new List<RaceSkillModificationAttribute>();

        var racesGUIDs = AssetDatabase.FindAssets("t: Race");
        foreach (var r in racesGUIDs)
        {
            var race = AssetDatabase.LoadAssetAtPath<Race>(AssetDatabase.GUIDToAssetPath(r));
            var modsGUIDs = AssetDatabase.FindAssets(race.RaceName + " t: SkillModifier");
            List<SkillModifier> mods = new List<SkillModifier>();
            foreach(var m in modsGUIDs)
                mods.Add(AssetDatabase.LoadAssetAtPath<SkillModifier>(AssetDatabase.GUIDToAssetPath(m)));

            RaceSkillModificationAttribute rsma = new RaceSkillModificationAttribute();
            rsma.Set(race, mods);
            raceSkillModifiers.Add(rsma);
        }

        Init();
    }
}
