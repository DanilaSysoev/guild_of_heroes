using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu
    (fileName = "NewSkill"
    , menuName = "ScriptableObjects/Skill"
    , order = 3)]
public class Skill : SerializedScriptableObject
{
    [SerializeField] 
    private string skillName;

    public string SkillName => skillName;
}
