using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryButton : MonoBehaviour
{
    [SerializeField]
    private SkillCategory skillCategory;
    public SkillCategory SkillCategory { get { return skillCategory; } }
    [SerializeField]
    private SkillList skillList;

    private void Start()
    {
        GetComponentInChildren<Text>().text = SkillCategory.CategoryName;
    }

    public void Click()
    {
        skillList.SetSkillCategory(SkillCategory);
    }
}
