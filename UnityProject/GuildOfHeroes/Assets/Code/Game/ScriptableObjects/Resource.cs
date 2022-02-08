using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu
    (fileName = "NewResource"
    , menuName = "ScriptableObjects/Resource"
    , order = 4)]
public class Resource : SerializedScriptableObject
{
    [SerializeField]
    private string resourceName;

    public string ResourceName => resourceName;
}
