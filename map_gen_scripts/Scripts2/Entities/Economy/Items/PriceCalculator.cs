using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Определяет базовый интерфейс для счетчика цены предмета
/// (с учетом качества и прочности).
/// Есть на каждом типе предмета
/// </summary>
public abstract class PriceCalculator : ScriptableObject
{
    /// <summary>
    /// Вычисляет цену предмета с учетом качества и прочности
    /// </summary>
    /// <param name="item">Предмет</param>
    /// <returns>Стоимость</returns>
    public abstract int CalculatePrice(Item item);
}
