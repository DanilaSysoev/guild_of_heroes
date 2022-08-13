using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill Id", menuName = "Mercenary/Ids/SkillId")]
public class SkillId : ScriptableObject
{
    [SerializeField]
    private string skillName;
    public string SkillName { get { return skillName; } }
        
    public static bool operator==(SkillId lo, SkillId ro)
    {
        return lo.skillName == ro.skillName;
    }
    public static bool operator!=(SkillId lo, SkillId ro)
    {
        return lo.skillName != ro.skillName;
    }
}
