using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mercenary : MonoBehaviour, IMercenary
{    
    public string Name { get; set; }

    [SerializeField]
    private List<Characteristic> characteristics;
    private Dictionary<CharacteristicId, Characteristic> characteristicsDict;
    public IReadOnlyList<ICharacteristic> Characteristics { get { return characteristics; } }

    [SerializeField]
    private List<Skill> skills;
    private Dictionary<SkillId, Skill> skillsDict;
    public IReadOnlyList<ISkill> Skills { get { return skills; } }

    [SerializeField]
    private List<Attribute> attributes;
    private Dictionary<AttributeId, Attribute> attributesDict;
    public IReadOnlyList<IAttribute> Attributes { get { return attributes; } }

    private Race race;
    public IRace Race { get { return race; } }

    private Class mercenaryClass;
    public IClass Class { get { return mercenaryClass; } }

    private int level = 1;
    public int Level { get { return level; } }

    private int experience;
    public int Experience { get { return experience; } }


    private bool isSetup;


    public void Setup()
    {
        if (!isSetup)
        {
            isSetup = true;
            skillsDict = new Dictionary<SkillId, Skill>();
            foreach (var s in skills)
            {
                skillsDict.Add(s.Id, s);
                s.Setup();
            }

            attributesDict = new Dictionary<AttributeId, Attribute>();
            foreach (var a in attributes)
            {
                attributesDict.Add(a.Id, a);
                a.Setup();
            }

            characteristicsDict = new Dictionary<CharacteristicId, Characteristic>();
            foreach (var c in characteristics)
            {
                characteristicsDict.Add(c.Id, c);
                c.Setup();
            }

            race.Setup();
            mercenaryClass.Setup();

            ApplyRaceModifiers();
            ApplyClassModifiers();
        }
        else
            throw new IncorrectSetupOrderException();
    }

    public IAttribute GetAttribute(AttributeId id)
    {
        if(!isSetup)
        {
            throw new InvalidOperationException("Mercenary isn't setup");
        }

        return attributesDict[id];
    }

    public ICharacteristic GetCharacteristic(CharacteristicId id)
    {
        if (!isSetup)
        {
            throw new InvalidOperationException("Mercenary isn't setup");
        }

        return characteristicsDict[id];
    }

    public ISkill GetSkill(SkillId id)
    {
        if (!isSetup)
        {
            throw new InvalidOperationException("Mercenary isn't setup");
        }

        return skillsDict[id];
    }

    internal void SetRace(Race race)
    {
        this.race = race;
    }
    internal void SetClass(Class mercenaryClass)
    {
        this.mercenaryClass = mercenaryClass;
    }

    public void AddExperience(int value)
    {
        experience += value;
        var lvl = ExperienceTable.Instance.GetLevelForExp(experience);
        for (int i = 0; i < lvl - level; ++i)
            LevelUp();
        level = lvl;
    }
    internal void LevelUp()
    {
        AttributesPointsAllocation();
        SkillPointsAllocation();
        
        NewFeaturesObtaining();
    }

    private void AttributesPointsAllocation()
    {
        foreach (var attr in Class.AttributesInLevelUp)
            GetAttribute(attr).IncreaseValue(Class.GetAttributeLevelUpValue(attr));
    }
    private void SkillPointsAllocation()
    {
        int skillPoints = Class.SkillPointPerLevel + GetCharacteristic(Class.SkillPointBonusCharacteristic).Modifier;
        float totalskillWeights = 0;
        foreach (var skill in All.Skills)
            totalskillWeights += Class.GetSkillLeveLUpPriority(skill);
        for(int i = 0; i < skillPoints; ++i)
        {
            float val = UnityEngine.Random.Range(0, totalskillWeights);
            float sum = 0;
            foreach(var skill in All.Skills)
            {
                float skillPriority = Class.GetSkillLeveLUpPriority(skill);
                if(skillPriority > 0 && val >= sum && val <= sum + skillPriority)
                {
                    GetSkill(skill).IncreaseValue(1);
                    break;
                }
                sum += skillPriority;
            }
        }
    }
    private void NewFeaturesObtaining()
    {
        print("НУЖНО ПРОПИСАТЬ ПОЛУЧЕНИЕ ФИЧ ПРИ ЛЕВЕЛ АПЕ!");
    }

    private void ApplyRaceModifiers()
    {
        foreach (var chId in race.DependentCharacteristicsIds)
            characteristicsDict[chId].IncreaseValue(race.GetCharacteristicModifier(chId));
    }
    private void ApplyClassModifiers()
    {
        foreach (var atId in mercenaryClass.DependentAttributesIds)
            attributesDict[atId].IncreaseValue(mercenaryClass.GetAttributeModifier(atId));

        foreach (var sId in mercenaryClass.DependentSkillsIds)
            skillsDict[sId].IncreaseValue(mercenaryClass.GetSkillModifier(sId));
    }
}
