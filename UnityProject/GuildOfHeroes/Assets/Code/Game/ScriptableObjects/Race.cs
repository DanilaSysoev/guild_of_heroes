using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu
    ( fileName = "NewRace"
    , menuName = "ScriptableObjects/Race"
    , order = 1)]
public class Race : SerializedScriptableObject
{
    [SerializeField]
    private string raceName;
    [SerializeField]
    private Dictionary<string, int> skillModifiers;
    [SerializeField]
    private bool isEvil;

    public string RaceName => raceName;
    public bool IsEvil => isEvil;

    public int GetSkillModifier(string skillName)
    {
        if(skillModifiers.ContainsKey(skillName))
            return skillModifiers[skillName];
        return 0;
    }
}
