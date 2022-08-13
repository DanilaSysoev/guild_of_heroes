using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITileResource
{
    string ResourceName { get; }
    Sprite Sprite { get; }
}
