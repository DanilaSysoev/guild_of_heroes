using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu
    ( fileName = "NewClass"
    , menuName = "ScriptableObjects/Class"
    , order = 2)]
public class Class : SerializedScriptableObject
{
    [SerializeField]
    private string className;
    [SerializeField]
    private Dictionary<string, int> skillModifiers;

    public string RaceName => className;

    public int GetSkillModifier(string skillName)
    {
        if (skillModifiers.ContainsKey(skillName))
            return skillModifiers[skillName];
        return 0;
    }
}
