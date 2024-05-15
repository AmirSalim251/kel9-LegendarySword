using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_TargetItem : MonoBehaviour
{
    public Controller_InventoryManager inventoryManager;
    public Controller_ItemSlot itemSelected;

    [Space]
    public GameObject targetPanel;
    public GameObject dummyPanel;

    public void SetItemSelected(Controller_ItemSlot itemSelected)
    {
        this.itemSelected = itemSelected;
    }

    public void UseToTarget(Controller_CharData charData)
    {
        if (itemSelected.stacks > 0)
        {
            if (itemSelected.item.UseItem(charData))
            {
                Debug.Log(itemSelected.name + " used");
                itemSelected.stacks -= 1;
                if(itemSelected.stacks <= 0)
                {
                    CloseTargetPanel();
                }
            }   
        }
        else
        {
            Debug.Log("Item habis");
        }
        inventoryManager.RefreshInventory();
    }

    public void OpenTargetPanel()
    {
        dummyPanel.SetActive(true);
        targetPanel.SetActive(true);
    }

    public void CloseTargetPanel()
    {
        itemSelected = null;
        dummyPanel.SetActive(false);
        targetPanel.SetActive(false);
    }
}
