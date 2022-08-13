using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Characteristic : ICharacteristic
{
    [SerializeField]
    private CharacteristicId id;
    public CharacteristicId Id { get { return id; } }

    public int Modifier { get { return (Value - 10) / 2; } }

    [SerializeField]
    private List<SkillId> dependentSkillsIds;
    public IReadOnlyList<SkillId> DependentSkillsIds { get { return dependentSkillsIds; } }

    [SerializeField]
    private List<AttributeId> dependentAttributesIds;
    public IReadOnlyList<AttributeId> DependentAttributesIds { get { return dependentAttributesIds; } }
    [SerializeField]
    private List<int> dependentAttributesCoefficients;


    [SerializeField]
    private Mercenary mercenary;
    public IMercenary Mercenary => throw new NotImplementedException();

    public string Name { get { return id.CharacteristicName; } }

    private int value;
    public int Value { get { return value; } }


    private bool isSetup;

    public void DecreaseValue(int delta)
    {
        SetValue(value + delta);
    }

    public void IncreaseValue(int delta)
    {
        SetValue(value - delta);
    }

    public void SetValue(int value)
    {
        if (!isSetup)
            throw new IncorrectSetupOrderException();

        RemoveModifiers();
        this.value = value;
        ApplyModifiers();
    }

    public void Setup()
    {
        if (!isSetup)
        {
            isSetup = true;
            ApplyModifiers();
        }
        else
            throw new IncorrectSetupOrderException();
    }

    private void ApplyModifierToSkills()
    {
        foreach (var sid in DependentSkillsIds)
            Mercenary.GetSkill(sid).IncreaseValue(Modifier);
    }
    private void RemoveModifiersFromSkills()
    {
        foreach (var sid in DependentSkillsIds)
            Mercenary.GetSkill(sid).DecreaseValue(Modifier);
    }
    private void ApplyModifierToAttributes()
    {
        for (int i = 0; i < dependentAttributesIds.Count; ++i)
            Mercenary.GetAttribute(dependentAttributesIds[i]).IncreaseValue(Modifier * dependentAttributesCoefficients[i]);
    }
    private void RemoveModifiersFromAttributes()
    {
        for (int i = 0; i < dependentAttributesIds.Count; ++i)
            Mercenary.GetAttribute(dependentAttributesIds[i]).DecreaseValue(Modifier * dependentAttributesCoefficients[i]);
    }

    private void ApplyModifiers()
    {
        ApplyModifierToSkills();
        ApplyModifierToAttributes();
    }
    private void RemoveModifiers()
    {
        RemoveModifiersFromSkills();
        RemoveModifiersFromAttributes();
    }
}
