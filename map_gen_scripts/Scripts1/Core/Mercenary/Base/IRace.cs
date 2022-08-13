using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRace : IMercenaryProperty
{
    IReadOnlyList<CharacteristicId> DependentCharacteristicsIds { get; }

    int GetCharacteristicModifier(CharacteristicId characteristicId);

    float GetClassPriority(IClass mercenaryClass);
}
