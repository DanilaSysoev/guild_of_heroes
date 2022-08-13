using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClass : IMercenaryProperty
{
    IReadOnlyList<SkillId> DependentSkillsIds { get; }
    IReadOnlyList<AttributeId> DependentAttributesIds { get; }
    IReadOnlyList<SkillId> LeveLUpPrioritySkillIds { get; }
    IReadOnlyList<AttributeId> AttributesInLevelUp { get; }

    int SkillPointPerLevel { get; }
    CharacteristicId SkillPointBonusCharacteristic { get; }

    int GetSkillModifier(SkillId skillId);
    float GetSkillLeveLUpPriority(SkillId skillId);
    int GetAttributeModifier(AttributeId attributeId);
    int GetAttributeLevelUpValue(AttributeId attributeId);
}
