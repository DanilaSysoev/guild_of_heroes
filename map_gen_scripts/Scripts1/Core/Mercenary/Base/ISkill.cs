using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill : IMercenaryParameter
{
    SkillId Id { get; }

    int BaseValue { get; }
}
