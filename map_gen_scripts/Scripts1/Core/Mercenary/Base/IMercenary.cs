using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMercenary
{
    string Name { get; }

    IReadOnlyList<ICharacteristic>    Characteristics { get; }
    IReadOnlyList<ISkill>             Skills { get; }
    IReadOnlyList<IAttribute>         Attributes { get; }

    IRace Race { get; }
    IClass Class { get; }

    int Level { get; }
    int Experience { get; }

    ICharacteristic GetCharacteristic(CharacteristicId id);
    ISkill          GetSkill(SkillId id);
    IAttribute      GetAttribute(AttributeId id);

    void Setup();

    void AddExperience(int value);
}
