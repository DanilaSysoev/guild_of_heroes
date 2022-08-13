using System;
using UnityEngine;

/// <summary>
/// Скриптовый объект для расы наемника
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "newRace", menuName = "New/Race")]
public class Race : ScriptableObject
{
    [SerializeField]
    private string raceName;
    /// <summary>
    /// Название расы
    /// </summary>
    public string RaceName { get { return raceName; } }

    /// <summary>
    /// Метод для Editor.
    /// Задает имя расы как имя файла ассета
    /// </summary>
    [ContextMenu("Set Default")]
    private void SetDefaultValues()
    {
        raceName = name;
    }

    public override string ToString()
    {
        return RaceName;
    }
}
