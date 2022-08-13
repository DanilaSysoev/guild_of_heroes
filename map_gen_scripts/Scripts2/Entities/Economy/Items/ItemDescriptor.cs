using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Базовые параметры некоторого предмета
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "newItem", menuName = "New/Item")]
public class ItemDescriptor : ScriptableObject
{
    [SerializeField]
    private List<ItemType> itemTypes;
    /// <summary>
    /// Типы предметов, к которым относится данный предмет
    /// </summary>
    public IReadOnlyList<ItemType> ItemTypes { get { return itemTypes; } }
    
    [SerializeField]
    private string itemName;
    /// <summary>
    /// Название предмета
    /// </summary>
    public string ItemName { get { return itemName; } }

    [SerializeField]
    private Sprite sprite;
    /// <summary>
    /// Спрайт предмета
    /// </summary>
    public Sprite Sprite { get { return sprite; } }

    [SerializeField]
    [Min(1)]
    private int maxStrength;
    /// <summary>
    /// Базовая максимальная прочность
    /// </summary>
    public int MaxStrength { get { return maxStrength; } }

    [SerializeField]
    private QualityUser strengthQualityUser;
    /// <summary>
    /// Применитель качества для прочности
    /// </summary>
    public QualityUser StrengthQualityUser { get { return strengthQualityUser; } }

    [SerializeField]
    private int basicPrice;
    /// <summary>
    /// Базовая стоимость предмета
    /// </summary>
    public int BasicPrice { get { return basicPrice; } }

    [SerializeField]
    private QualityUser priceQualityUser;
    /// <summary>
    /// Применитель качества для стоимости
    /// </summary>
    public QualityUser PriceQualityUser { get { return priceQualityUser; } }

    [SerializeField]
    private PriceCalculator priceCalculator;
    /// <summary>
    /// Вычислитель стоимости для предмета
    /// </summary>
    public PriceCalculator PriceCalculator { get { return priceCalculator; } }

    /// <summary>
    /// Проверяет, относится ли предмет к указанному типу
    /// </summary>
    /// <param name="type">Тип предмета</param>
    /// <returns>Результат проверки</returns>
    public bool IsType(ItemType type)
    {
        return itemTypes.Any(t => t.IsType(type));
    }

    public override string ToString()
    {
        return itemName;
    }

    /// <summary>
    /// Метод для Editor.
    /// Задает имя предмета как имя файла ассета
    /// </summary>
    [ContextMenu("SetDefault")]
    public void SetDefaultName()
    {
        itemName = name;
    }
}
