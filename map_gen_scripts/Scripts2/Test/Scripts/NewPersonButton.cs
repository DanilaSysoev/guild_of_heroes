using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPersonButton : MonoBehaviour
{
    [SerializeField]
    private PersonBuilder personBuilder;
    [SerializeField]
    private PersonList personList;

    public void Click()
    {
        var p = personBuilder.BuildPerson();

        personList.AddPerson(p.GetComponent<Person>());
    }
}
