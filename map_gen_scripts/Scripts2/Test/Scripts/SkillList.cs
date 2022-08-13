using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkillList : MonoBehaviour
{
    [SerializeField]
    private RectTransform viewPanel;
    [SerializeField]
    private GameObject skillViewPrefab;
    [SerializeField]
    private GridLayoutGroup gridLayout;

    private List<GameObject> skillsViews;

    private Person person;
    private SkillCategory skillCategory;
    
    [SerializeField]
    private Image personImage;
    [SerializeField]
    private Text raceText;
    [SerializeField]
    private Text classText;

    private void Start()
    {
        skillsViews = new List<GameObject>();
    }

    public void SetPerason(Person person)
    {
        this.person = person;
        personImage.sprite = person.Sprite;
        personImage.color = Color.white;
        raceText.text = "Race: " + person.Race;
        classText.text = "Class: " + person.Class;
        if (skillCategory != null)
            BuildList();
    }
    public void SetSkillCategory(SkillCategory skillCategory)
    {
        this.skillCategory = skillCategory;
        if (person != null)
            BuildList();
    }

    private void BuildList()
    {
        List<Skill> skills = person.Skills.Where(kv => kv.Key.Category == skillCategory).Select(kv => kv.Key).ToList();
        List<int> values = person.Skills.Where(kv => kv.Key.Category == skillCategory).Select(kv => kv.Value).ToList();

        foreach (var sv in skillsViews)
            Destroy(sv);

        skillsViews.Clear();

        for(int i = 0; i < skills.Count; ++i)
        {
            var sv = Instantiate(skillViewPrefab, viewPanel);
            sv.GetComponent<SkillView>().SetSkill(skills[i], values[i]);

            skillsViews.Add(sv);
        }
        viewPanel.sizeDelta = new Vector2(viewPanel.sizeDelta.x, ((skillViewPrefab.transform as RectTransform).sizeDelta.y + gridLayout.spacing.y) * skillsViews.Count + 
                                                                   gridLayout.padding.bottom + gridLayout.padding.top);
    }


}
