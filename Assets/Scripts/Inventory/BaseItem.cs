using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItem : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;

    public abstract ItemType GetItemType();
    public abstract void UseItem();

}

public enum ItemType
{
    Materials,
    Consumable,
    Equipment
}


