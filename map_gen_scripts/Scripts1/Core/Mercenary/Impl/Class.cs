using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New class", menuName = "Mercenary/MercanaryClass")]
public class Class : ScriptableObject, IClass
{
    [SerializeField]
    private string className;
    public string Name { get { return className; } }

    [SerializeField]
    private List<SkillId> dependentSkillsIds;
    [SerializeField]
    private List<int> skillModifiers;
    private Dictionary<SkillId, int> skillModifiersDict;
    public IReadOnlyList<SkillId> DependentSkillsIds { get { return dependentSkillsIds; } }
    
    [SerializeField]
    private List<AttributeId> dependentAttributesIds;
    [SerializeField]
    private List<int> attributeModifiers;
    private Dictionary<AttributeId, int> attributeModifiersDict;
    public IReadOnlyList<AttributeId> DependentAttributesIds { get { return dependentAttributesIds; } }
    
    [SerializeField]
    private List<AttributeId> attributesInLevelUp;
    [SerializeField]
    private List<int> attributesInLevelUpValues;
    private Dictionary<AttributeId, int> AttributesInLevelUpDict;
    public IReadOnlyList<AttributeId> AttributesInLevelUp { get { return attributesInLevelUp; } }

    [SerializeField]
    private List<SkillId> leveLUpPrioritySkillIds;
    [SerializeField]
    private List<float> skillPriorities;
    private Dictionary<SkillId, float> skillPrioritiesDict;
    public IReadOnlyList<SkillId> LeveLUpPrioritySkillIds { get { return dependentSkillsIds; } }

    [SerializeField]
    private int skillPointPerLevel;
    public int SkillPointPerLevel { get { return skillPointPerLevel; } }
    
    [SerializeField]
    private CharacteristicId skillPointBonusCharacteristic;
    public CharacteristicId SkillPointBonusCharacteristic { get { return skillPointBonusCharacteristic; } }

    private bool isSetup;

    public int GetAttributeModifier(AttributeId attributeId)
    {
        if (attributeModifiersDict.ContainsKey(attributeId))
            return attributeModifiersDict[attributeId];

        return 0;
    }

    public int GetSkillModifier(SkillId skillId)
    {
        if (skillModifiersDict.ContainsKey(skillId))
            return skillModifiersDict[skillId];

        return 0;
    }

    public void Setup()
    {
        if (!isSetup)
        {
            isSetup = true;

            skillModifiersDict = new Dictionary<SkillId, int>();
            for (int i = 0; i < dependentSkillsIds.Count; ++i)
                skillModifiersDict.Add(dependentSkillsIds[i], skillModifiers[i]);

            skillPrioritiesDict = new Dictionary<SkillId, float>();
            for (int i = 0; i < leveLUpPrioritySkillIds.Count; ++i)
                skillPrioritiesDict.Add(leveLUpPrioritySkillIds[i], skillPriorities[i]);

            attributeModifiersDict = new Dictionary<AttributeId, int>();
            for (int i = 0; i < dependentAttributesIds.Count; ++i)
                attributeModifiersDict.Add(dependentAttributesIds[i], attributeModifiers[i]);

            AttributesInLevelUpDict = new Dictionary<AttributeId, int>();
            for (int i = 0; i < attributesInLevelUp.Count; ++i)
                AttributesInLevelUpDict.Add(attributesInLevelUp[i], attributesInLevelUpValues[i]);
        }
    }

    public float GetSkillLeveLUpPriority(SkillId skillId)
    {
        if (skillPrioritiesDict.ContainsKey(skillId))
            return skillPrioritiesDict[skillId];

        return 1;
    }

    public int GetAttributeLevelUpValue(AttributeId attributeId)
    {
        if (AttributesInLevelUpDict.ContainsKey(attributeId))
            return AttributesInLevelUpDict[attributeId];

        return 0;
    }
}
