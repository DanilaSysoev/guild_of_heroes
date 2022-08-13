using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Attribute Id", menuName = "Mercenary/Ids/AttributeId")]
public class AttributeId : ScriptableObject
{
    [SerializeField]
    private string attributeName;
    public string AttributeName { get { return attributeName; } }

    public static bool operator ==(AttributeId lo, AttributeId ro)
    {
        return lo.attributeName == ro.attributeName;
    }
    public static bool operator !=(AttributeId lo, AttributeId ro)
    {
        return lo.attributeName != ro.attributeName;
    }
}
