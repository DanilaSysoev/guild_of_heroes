using UnityEngine;
using System;

/// <summary>
/// Скриптовый объект для скила наемника
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "newSkill", menuName = "New/Skill")]
public class Skill : ScriptableObject
{
    [SerializeField]
    private SkillCategory category;
    /// <summary>
    /// Категория скила
    /// </summary>
    public SkillCategory Category { get { return category; } }

    [SerializeField]
    private string skillName;
    /// <summary>
    /// Название скила
    /// </summary>
    public string SkillName { get { return skillName; } }

    /// <summary>
    /// Метод для Editor.
    /// Задает имя скила как имя файла ассета
    /// </summary>
    [ContextMenu("Set Default")]
    private void SetDefaultValues()
    {
        skillName = name;
    }
}
