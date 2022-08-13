using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Скриптовый объект, описывающий тип предмета
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "newItemType", menuName = "New/Item type")]
public class ItemType : ScriptableObject
{
    [SerializeField]
    private string itemTypeName;
    public string ItemTypeName { get { return itemTypeName; } }

    [SerializeField]
    private List<ItemType> parentTypes;
    public IReadOnlyList<ItemType> ParentTypes { get { return parentTypes; } }

    [SerializeField]
    private Sprite sprite;
    public Sprite Sprite { get { return sprite; } }

    public override string ToString()
    {
        return itemTypeName;
    }

    [ContextMenu("SetDefault")]
    public void SetDefaultName()
    {
        itemTypeName = name;
    }

    public bool IsType(ItemType type)
    {
        return this == type || parentTypes.Any(t => t.IsType(type));
    }
}
