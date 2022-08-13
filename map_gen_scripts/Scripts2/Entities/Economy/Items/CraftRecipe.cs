using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Инкапсулирует атрибут (слот) рецепта на создание предмета
/// </summary>
[Serializable]
public class CraftRecipeAttribute
{
    [SerializeField]
    private ItemDescriptor item;
    /// <summary>
    /// Прототип предмета
    /// </summary>
    public ItemDescriptor Item { get { return item; } }

    [SerializeField]
    private int count;
    /// <summary>
    /// Количество предметов
    /// </summary>
    public int Count { get { return count; } }

    [SerializeField]
    private float minQuality;
    /// <summary>
    /// Минимальное допустимое качество
    /// </summary>
    public float MinQuality { get { return minQuality; } }

    [SerializeField]
    [Min(0)]
    private float coefficient;
    /// <summary>
    /// Вспомогательны коеффициент
    /// (может использоваться, например, в подсчете качества)
    /// </summary>
    public float Coefficient { get { return coefficient; } }
}

/// <summary>
/// Инкапсулирует требования к скилу для применения рецепта
/// </summary>
[Serializable]
public class SkillReuirement
{
    [SerializeField]
    private Skill skill;
    /// <summary>
    /// Скил
    /// </summary>
    public Skill Skill { get { return skill; } }
    [SerializeField]
    private int minValue;
    /// <summary>
    /// Минимальное значение скила
    /// </summary>
    public int MinValue { get { return minValue; } }
}

/// <summary>
/// Описывает рецепт на создание предмета
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "newCraftRecipe", menuName = "New/Craft recipe")]
public class CraftRecipe : ScriptableObject
{
    [SerializeField]
    private List<SkillReuirement> skillReuirements;
    /// <summary>
    /// Требования к скилам
    /// </summary>
    public IReadOnlyList<SkillReuirement> SkillReuirements { get { return skillReuirements; } }    

    [SerializeField]
    private List<CraftRecipeAttribute> resources;
    /// <summary>
    /// Необходимые ресурсы
    /// </summary>
    public IReadOnlyList<CraftRecipeAttribute> Resources { get { return resources; } }

    [SerializeField]
    private List<CraftRecipeAttribute> products;
    /// <summary>
    /// Получаемые продукты
    /// </summary>
    public IReadOnlyList<CraftRecipeAttribute> Products { get { return products; } }

    [SerializeField]
    private RecipeQualityCalculator qualityCalculator;
    /// <summary>
    /// Объект для рассчета качества
    /// </summary>
    public RecipeQualityCalculator QualityCalculator { get { return qualityCalculator; } }
}
