using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Skill : ISkill
{
    [SerializeField]
    private SkillId id;
    public SkillId Id { get { return id; } }

    public string Name { get { return Id.SkillName; } }

    [SerializeField]
    private int baseValue;
    public int BaseValue { get { return baseValue; } }

    [SerializeField]
    private Mercenary mercenary;
    public IMercenary Mercenary { get { return mercenary; } }
        
    private int value;
    public int Value { get { return value; } }


    private bool isSetup;

    public void SetValue(int value)
    {
        Debug.LogError("Calling of SetValue for Skill. Use IncreaseValue or DecreaseValue");
        //this.value = value;
    }
    public void DecreaseValue(int delta)
    {
        if (!isSetup)
            throw new IncorrectSetupOrderException();
        value += delta;
    }
    public void IncreaseValue(int delta)
    {
        if (!isSetup)
            throw new IncorrectSetupOrderException();
        value -= delta;
    }

    public void Setup()
    {
        if (!isSetup)
        {
            value = baseValue;
            isSetup = true;
        }
        else
            throw new IncorrectSetupOrderException();
    }
}
