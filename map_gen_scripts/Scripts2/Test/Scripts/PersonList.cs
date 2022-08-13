using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonList : MonoBehaviour
{
    [SerializeField]
    private RectTransform viewPanel;
    [SerializeField]
    private GameObject personButtonPrefab;
    [SerializeField]
    private GridLayoutGroup gridLayout;
    [SerializeField]
    private SkillList skillList;


    private List<Person> persons;
    private List<PersonButton> personsButtons;

    private void Start()
    {
        persons = new List<Person>();
        personsButtons = new List<PersonButton>();
    }

    public void AddPerson(Person person)
    {
        var pb = Instantiate(personButtonPrefab, viewPanel);
        pb.GetComponent<PersonButton>().Person = person;
        pb.GetComponent<PersonButton>().SkillList = skillList;

        persons.Add(person);
        personsButtons.Add(pb.GetComponent<PersonButton>());
        
        viewPanel.sizeDelta = new Vector2(viewPanel.sizeDelta.x, ((pb.transform as RectTransform).sizeDelta.y + gridLayout.spacing.y) * persons.Count + gridLayout.padding.bottom + gridLayout.padding.top);
    }

    public void RemovePerson()
    { }
}
