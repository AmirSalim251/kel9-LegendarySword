using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_ItemDictionary
{

    [SerializeField] private string[] _specificPath = { "Consumable", "Materials", "Equipment" };
    private Dictionary<string, BaseItem> _itemDictionary = new Dictionary<string, BaseItem>();

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        foreach (string path in _specificPath)
        {
            AttachMaterials(path);
        }
    }

    private void AttachMaterials(string path)
    {
        BaseItem[] items = Resources.LoadAll<BaseItem>("Items/" + path);
        /*_itemDictionary.Clear(); // Consider clearing the dictionary outside this loop if you do not intend to reset it each time*/

        foreach (BaseItem item in items)
        {
            if (item != null)
            {
                _itemDictionary.Add(item.name, item);
                // Debug.Log("Added " + item.name);
            }
        }
    }

    public BaseItem GetValueByKey(string key)
    {
        BaseItem item;
        if (_itemDictionary.TryGetValue(key, out item))
        {
            return item;
        }
        else
        {
            Debug.LogWarning("Key not found in dictionary: " + key);
            return null;
        }
    }
}

