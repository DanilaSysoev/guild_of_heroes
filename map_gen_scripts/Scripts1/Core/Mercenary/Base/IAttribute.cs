using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttribute : IMercenaryParameter
{
    AttributeId Id { get; }

    int CurrentValue { get; }
}
