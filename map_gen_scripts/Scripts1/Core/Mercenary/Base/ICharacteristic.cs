using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacteristic : IMercenaryParameter
{
    CharacteristicId Id { get; }

    int Modifier { get; }

    IReadOnlyList<SkillId> DependentSkillsIds { get; }
    IReadOnlyList<AttributeId> DependentAttributesIds { get; }
}
