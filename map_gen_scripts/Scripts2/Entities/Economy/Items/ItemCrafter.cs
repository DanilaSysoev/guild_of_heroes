using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Простая реализация создателя предметов.
/// Создает предметы, рассчитывает качество калькулятором 
/// и распределяет качество по продуктам
/// </summary>
public class ItemCrafter : MonoBehaviour, IItemCrafter
{
    /// <summary>
    /// Создает предметы из ингредиентов
    /// </summary>
    /// <param name="recipe">Рецепт</param>
    /// <param name="resources">Ингредиенты по слотам рецепта</param>
    /// <returns>Продукты</returns>
    public List<Item> CraftItems(CraftRecipe recipe, List<List<Item>> resources)
    {
        // Рассчитывает качество с помощью калькулятора
        ////////////////////////////////////////////////
        var qts = recipe.QualityCalculator.CalculateQualities(recipe, resources);
        ////////////////////////////////////////////////
        
        List<Item> result = new List<Item>();

        // Для всех слотов продуктов и для всех единиц продуктов
        // создет продукт указанного качества из указанного прототипа
        ///////////////////////////////////////////////////////////////
        for(int pi = 0; pi < recipe.Products.Count; ++pi)
            for (int i = 0; i < recipe.Products[pi].Count; ++i)
                result.Add(new Item(recipe.Products[pi].Item, qts[pi]));
        ///////////////////////////////////////////////////////////////

        return result;
    }
}
