using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItem : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;

    public int itemAmount;
    public int maxQuantity;

    public abstract ItemType GetItemType();
    public abstract bool UseItem(Controller_CharData charTarget);

    public static implicit operator BaseItem(Controller_ItemSlot v)
    {
        throw new NotImplementedException();
    }
}

public enum ItemType
{
    Consumable,
    Materials,
    Equipment
}


