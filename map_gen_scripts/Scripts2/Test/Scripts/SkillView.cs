using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillView : MonoBehaviour
{
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text valueText;

    public void SetSkill(Skill skill, int val)
    {
        nameText.text = skill.SkillName;
        valueText.text = ": " + val;
    }
}
