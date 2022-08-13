using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Скриптовый категории скилов наемника
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "newSkillCategory", menuName = "New/Skill Category")]
public class SkillCategory : ScriptableObject
{
    [SerializeField]
    private string categoryName;
    /// <summary>
    /// Название категории скилов
    /// </summary>
    public string CategoryName { get { return categoryName; } }

    /// <summary>
    /// Метод для Editor.
    /// Задает имя категории скилов как имя файла ассета
    /// </summary>
    [ContextMenu("Set Default")]
    private void SetDefaultValues()
    {
        categoryName = name;
    }
}
