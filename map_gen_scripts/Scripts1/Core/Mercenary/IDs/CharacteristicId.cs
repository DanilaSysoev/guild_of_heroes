using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Characteristic Id", menuName = "Mercenary/Ids/CharacteristicId")]
public class CharacteristicId : ScriptableObject
{
    [SerializeField]
    private string characteristicName;
    public string CharacteristicName { get { return characteristicName; } }

    public static bool operator ==(CharacteristicId lo, CharacteristicId ro)
    {
        return lo.characteristicName == ro.characteristicName;
    }
    public static bool operator !=(CharacteristicId lo, CharacteristicId ro)
    {
        return lo.characteristicName != ro.characteristicName;
    }
}
