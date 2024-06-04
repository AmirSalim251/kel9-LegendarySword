using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory/Items/Database")]
public class ItemDatabaseObject : ScriptableObject
{
    public BaseItem[] Items;
    public Dictionary<BaseItem, int> GetId = new Dictionary<BaseItem, int>();

    public void OnAfterDeserialize()
    {
        
    }
    
    public void OnBeforeDeserialize() 
    {
        
    }

}
