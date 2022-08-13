using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Простой линейный калькулятор стоимости.
/// Вычисляет стоимость пропорционально прочности.
/// Стоимость предмета прочности 0 задается коэффициентом zeroStrengthPriceCoeff
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "newLinearPriceCalculator", menuName = "New/LinearPriceCalculator")]
public class LinearPriceCalculator : PriceCalculator
{
    /// <summary>
    /// Коэффициент стоимости предмета прочности 0
    /// </summary>
    [SerializeField]
    private float zeroStrengthPriceCoeff = .05f;

    /// <summary>
    /// Вычисляет стоимость
    /// </summary>
    /// <param name="item">Предмет</param>
    /// <returns>Стоимость</returns>
    public override int CalculatePrice(Item item)
    {
        return (int)(item.MaxPrice * (item.Strength + zeroStrengthPriceCoeff * (item.MaxStrength - item.Strength)) / item.MaxStrength);
    }
}
