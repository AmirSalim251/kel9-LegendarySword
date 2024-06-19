using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Controller_ItemSlot
{
    public BaseItem item;
    public string name;
    public int stacks;

    public Controller_ItemSlot(BaseItem newItem, int newStacks)
    {
        item = newItem;
        stacks = newStacks;
    }
}
