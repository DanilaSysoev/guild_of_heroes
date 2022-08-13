using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Экземпляр предмета
/// </summary>
public class Item
{
    /// <summary>
    /// Дескриптор предмета (базовые параметры)
    /// </summary>
    private ItemDescriptor  itemDescriptor;
    private float           quality;
    private int             maxStrength;
    private int             strength;
    private int             maxPrice;

    /// <summary>
    /// Возвращает список типов предмета
    /// </summary>
    public IReadOnlyList<ItemType>  ItemTypes   { get { return itemDescriptor.ItemTypes; } }
    /// <summary>
    /// Возвращает название предмета
    /// </summary>
    public string                   Name        { get { return itemDescriptor.ItemName; } }
    /// <summary>
    /// Возвращает спрайт предмета
    /// </summary>
    public Sprite                   Sprite      { get { return itemDescriptor.Sprite; } }
    /// <summary>
    /// Возвращает качество предмета
    /// </summary>
    public float                    Quality     { get { return quality; } }
    /// <summary>
    /// Возвращает максимальную прочность предмета
    /// </summary>
    public int                      MaxStrength { get { return maxStrength; } }
    /// <summary>
    /// Возвращает текущую прочность предмета
    /// </summary>
    public int                      Strength    { get { return strength; } }
    /// <summary>
    /// Проверяет, сломан предмет или нет
    /// </summary>
    public bool                     IsBroken    { get { return strength <= 0; } }
    /// <summary>
    /// Возвращает максимальную стоимость предмета (с прочностью 100)
    /// </summary>
    public int                      MaxPrice    { get { return maxPrice; } }
    /// <summary>
    /// Возвращает текущую стоимость предмета (используя калькулятор стоимости предмета)
    /// </summary>
    public int                      Price       { get { return itemDescriptor.PriceCalculator.CalculatePrice(this); } }

    /// <summary>
    /// Создает предмет заданного качества.
    /// Максимальная прочность и максимальная стоимость
    /// вычисляются с использованием соответствующих QualityUser
    /// </summary>
    /// <param name="item">Базовые параметры предмета</param>
    /// <param name="quality">Качество предмета</param>
    public Item(ItemDescriptor item, float quality)
    {
        this.itemDescriptor = item;
        this.quality = quality;
        maxStrength = (int)item.StrengthQualityUser.ApplyQuality(item.MaxStrength, quality);
        strength = maxStrength;
        maxPrice = (int)item.PriceQualityUser.ApplyQuality(item.BasicPrice, quality);
    }
    
    /// <summary>
    /// Создает предмет заданного качества и цены.
    /// Максимальная прочность вычисляется с использованием соответствующео QualityUser
    /// </summary>
    /// <param name="item">Базовые параметры предмета</param>
    /// <param name="quality">Качество предмета</param>
    /// <param name="price">Максимальная цена</param>
    public Item(ItemDescriptor item, float quality, int price)
    {
        this.itemDescriptor = item;
        this.quality = quality;
        maxStrength = (int)item.StrengthQualityUser.ApplyQuality(item.MaxStrength, quality);
        strength = maxStrength;
        this.maxPrice = price;
    }

    /// <summary>
    /// Создает предмет заданного качества и максимальной прочности.
    /// Максимальная стоимость вычисляется с использованием соответствующео QualityUser
    /// </summary>
    /// <param name="item">Базовые параметры предмета</param>
    /// <param name="quality">Качество предмета</param>
    /// <param name="price">Максимальная цена</param>
    public Item(ItemDescriptor item, int maxStrength, float quality)
    {
        this.itemDescriptor = item;
        this.quality = quality;
        this.maxStrength = maxStrength;
        strength = maxStrength;

        maxPrice = (int)item.PriceQualityUser.ApplyQuality(item.BasicPrice, quality);
    }

    /// <summary>
    /// Создает предмет заданного качества, прочности и цены.
    /// QualityUser не используются
    /// </summary>
    /// <param name="item">Базовые параметры предмета</param>
    /// <param name="quality">Качество предмета</param>
    /// <param name="price">Максимальная цена</param>
    /// <param name="maxStrength">Максимальная прочность</param>
    public Item(ItemDescriptor item, float quality, int price, int maxStrength)
    {
        this.itemDescriptor = item;
        this.quality = quality;
        this.maxStrength = maxStrength;
        strength = maxStrength;
        this.maxPrice = price;
    }

    /// <summary>
    /// Проверяет, относится ли предмет к указанному типу
    /// </summary>
    /// <param name="type">Тип предмета</param>
    /// <returns>Результат проверки</returns>
    public bool IsType(ItemType type)
    {
        return itemDescriptor.IsType(type);
    }

    public override string ToString()
    {
        return Name;
    }
}
