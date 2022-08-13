using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Базовай абстрактный класс для чего-то, что использует качество.
/// Определяет интерфейс применения качества к исходному значению
/// </summary>
public abstract class QualityUser : ScriptableObject
{
    public abstract float ApplyQuality(float val, float rawQuality);
}
