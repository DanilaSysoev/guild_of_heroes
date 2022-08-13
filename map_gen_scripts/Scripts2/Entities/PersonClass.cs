using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Скриптовый объект для класса наемника
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "newPersonClass", menuName = "New/Person class")]
public class PersonClass : ScriptableObject
{
    [SerializeField]
    private string className;
    /// <summary>
    /// Название класса
    /// </summary>
    public string ClassName { get { return className; } }

    /// <summary>
    /// Метод для Editor.
    /// Задает имя класса как имя файла ассета
    /// </summary>
    [ContextMenu("Set Default")]
    private void SetDefaultValues()
    {
        className = name;
    }

    public override string ToString()
    {
        return ClassName;
    }
}
