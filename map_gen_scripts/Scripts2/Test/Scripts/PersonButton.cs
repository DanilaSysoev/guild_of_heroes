using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonButton : MonoBehaviour
{
    private Person person;
    public Person Person
    {
        get { return person; } 
        set 
        {
            person = value;
            GetComponentInChildren<Text>().text = value.name;
        } 
    }
    public SkillList SkillList { get; set; }

    public void Click()
    {
        SkillList.SetPerason(person);
    }
}
