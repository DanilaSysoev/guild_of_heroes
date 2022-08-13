using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Базовый интерфейс для создателя предметов по рецепту
/// </summary>
public interface IItemCrafter
{
    /// <summary>
    /// Создает предметы из указанных ингредиентов по указанному рецепту
    /// </summary>
    /// <param name="recipe">Рецепт</param>
    /// <param name="resources">Ингредиенты</param>
    /// <returns>Продукты</returns>
    List<Item> CraftItems(CraftRecipe recipe, List<List<Item>> resources);
}
