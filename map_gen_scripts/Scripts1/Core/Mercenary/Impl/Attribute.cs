using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Attribute : IAttribute
{
    [SerializeField]
    private AttributeId id;
    public AttributeId Id { get { return id; } }

    private int currentValue;
    public int CurrentValue { get { return currentValue; } }

    [SerializeField]
    private Mercenary mercenary;
    public IMercenary Mercenary { get { return mercenary; } }
    
    public string Name { get { return id.AttributeName; } }

    [SerializeField]
    private int value;
    public int Value { get { return value; } }


    private bool isSetup;

    public void DecreaseValue(int delta)
    {
        if (!isSetup)
            throw new IncorrectSetupOrderException();

        value -= delta;
        if (currentValue > value)
            currentValue = value;
    }

    public void IncreaseValue(int delta)
    {
        if (!isSetup)
            throw new IncorrectSetupOrderException();

        value += delta;
        currentValue += delta;
    }

    public void SetValue(int value)
    {
        Debug.LogError("Calling of SetValue for Attribute. Use IncreaseValue or DecreaseValue");
    }

    public void Setup()
    {
        if(!isSetup)
        {
            isSetup = true;
            currentValue = value;
        }
        else
            throw new IncorrectSetupOrderException();
    }
}
