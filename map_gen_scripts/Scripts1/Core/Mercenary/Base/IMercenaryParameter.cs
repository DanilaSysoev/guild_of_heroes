using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMercenaryParameter : IMercenaryProperty
{
    int Value { get; }
    IMercenary Mercenary { get; }

    void SetValue(int value);
    void IncreaseValue(int delta);
    void DecreaseValue(int delta);
}
