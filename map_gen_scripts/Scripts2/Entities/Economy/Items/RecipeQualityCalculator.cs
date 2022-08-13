using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Базовый абстрактный класс для рассчета качества продуктов рецепта.
/// Каждый рецепт обладает объектом конкретной реализациеи данного интерфейса.
/// </summary>
public abstract class RecipeQualityCalculator : ScriptableObject
{
    /// <summary>
    /// Рассчитывает качество продуктов рецепта
    /// </summary>
    /// <param name="recipe">Рецепт</param>
    /// <param name="resources">Ресурсы</param>
    /// <returns>Список качества</returns>
    public abstract List<float> CalculateQualities(CraftRecipe recipe, List<List<Item>> resources);
}
