using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Controller_InventoryManager : MonoBehaviour
{
    public GameObject inventPanel;
    public InvenType inventoryType;

    [SerializeReference] public List<Controller_ItemSlot> items = new List<Controller_ItemSlot>();

    [SerializeField] public List<Controller_PanelItem> _existingPanels = new List<Controller_PanelItem>();

    [SerializeField] public GameObject[] _itemPanelGrid;

    [SerializeField] private ControllerMouse _itemSlotMouse;

    public ControllerMouse mouse { get => _itemSlotMouse; }
    public Controller_ItemDictionary itemDictionary;

    [SerializeField] private int _inventorySize;

    public GameObject selectedItem;

    public Sprite itemAvail;
    public Sprite itemUnavail;

    public Sprite itemAvailTopRow;
    public Sprite itemUnavailTopRow;

    private void Awake()
    {
        itemDictionary = new Controller_ItemDictionary();
        itemDictionary.Initialize();
        Initialize();
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseConsumable()
    {

    }

    public void RefreshInventory()
    {
        int index = 0;
        foreach (Controller_ItemSlot i in items)
        {
            //Name our List Elements
            i.name = "" + (index + 1);
            if (i.item != null) { i.name += ": " + i.item.itemName; }
            else { i.name += ": -"; }

            //Update our Panels
            Controller_PanelItem panel = _existingPanels[index];
            panel.name = i.name + " Panel";
            if (panel != null)
            {
                panel.inventoryManager = this;
                panel.itemSlot = i;
                if (i.item != null)
                {
                    panel.panelImage.sprite = itemAvailTopRow;
                    panel.itemImage.gameObject.SetActive(true);
                    panel.itemImage.sprite = i.item.itemIcon;
                    panel.itemImage.CrossFadeAlpha(1, 0.05f, true);
                    panel.quantityText.gameObject.SetActive(true);
                    panel.itemText.gameObject.SetActive(true);
                    panel.itemText.SetText(i.item.itemName);

                    panel.slotType = i.item.GetItemType();
                    

                    if (i.stacks > 1)
                    {
                        panel.quantityText.text = "" + i.stacks;
                    }
                    else if(i.stacks == 1)
                    {
                        panel.quantityText.gameObject.SetActive(false);
                    }
                    else if(i.stacks == 0)
                    {
                        panel.panelImage.sprite = itemUnavailTopRow;
                        panel.itemText.gameObject.SetActive(false);
                        panel.quantityText.gameObject.SetActive(false);
                        panel.itemImage.sprite = null;
                        panel.itemImage.gameObject.SetActive(false);
                        i.item = null;
                        
                    }
                }

                if (i.item == null)
                {
                    panel.panelImage.sprite = itemUnavailTopRow;
                    panel.itemText.gameObject.SetActive(false);
                    panel.quantityText.gameObject.SetActive(false);
                    panel.itemImage.sprite = null;
                }
            }
            index++;
        }

        /*_itemSlotMouse.EmptySlot();*/
        
    }

    public int AddItem(BaseItem item, int amount)
    {

        if (item == null)
        {
            Debug.Log("Could not find Item in Dictionary to add to Inventory");
            return amount;
        }

        //Check for open spaces in existing slots
        foreach (Controller_ItemSlot i in items)
        {
            if (i.item != null)
            {
                if (i.item.itemName == item.itemName)
                {
                    if (amount > i.item.maxQuantity - i.stacks)
                    {
                        amount -= i.item.maxQuantity - i.stacks;
                        i.stacks = i.item.maxQuantity;
                    }
                    else
                    {
                        i.stacks += amount;
                        /*if (_inventoryMenu.activeSelf) RefreshInventory();*/
                        RefreshInventory();
                        return 0;
                    }
                }
            }
        }
        //Fill empty slots with leftover items
        foreach (Controller_ItemSlot i in items)
        {
            if (i.item == null)
            {
                if (amount > item.maxQuantity)
                {
                    i.item = item;
                    i.stacks = item.maxQuantity;
                    amount -= item.maxQuantity;
                }
                else
                {
                    i.item = item;
                    i.stacks = amount;
                    RefreshInventory();
                    return 0;
                }
            }
        }

        //No space in Inventory, return remainder items
        Debug.Log("No space in Inventory for: " + item.itemName);
        return amount;
    }

    private void Initialize()
    {
        for (int i = 0; i < _itemPanelGrid.Length; i++)
        {
            Controller_PanelItem[] itemPanelsInGrid = _itemPanelGrid[i].GetComponentsInChildren<Controller_PanelItem>();
            _existingPanels.AddRange(itemPanelsInGrid);
            
        }

        LoadInventory();

        if(items.Count == 0)
        {
            for (int i = 0; i < _existingPanels.Count; i++)
            {
                items.Add(new Controller_ItemSlot(null, 0));
                _existingPanels[i].mouse = mouse;
            }
            SetDefaultConsumablePouch();
            _inventorySize = _existingPanels.Count;
        }
        
        
    }

    private void SetDefaultConsumablePouch()
    {
        AddItem(itemDictionary.GetValueByKey("Potion"), 3);
        AddItem(itemDictionary.GetValueByKey("Elixir"), 3);
        AddItem(itemDictionary.GetValueByKey("Soulstone"), 3);
        RefreshInventory();
    }

    public void MoveSlot(Controller_ItemSlot itemSlot1, Controller_ItemSlot itemSlot2)
    {
        if (itemSlot2 != null)
        {
            Controller_ItemSlot temp = new Controller_ItemSlot(itemSlot1.item, itemSlot1.stacks);

            itemSlot1.item = itemSlot2.item;
            itemSlot1.name = itemSlot2.name;
            itemSlot1.stacks = itemSlot2.stacks;


            itemSlot2.item = temp.item;
            itemSlot2.name = temp.name;
            itemSlot2.stacks = temp.stacks;

            Debug.Log(itemSlot1.item);
            Debug.Log(itemSlot2.item);
        }
        else
        {
            Debug.Log("gagal");
        }

    }

    /*private void AttachDefaultItem(int targetSlot)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item == null) continue;

            if (items[i].item.GetItemType() == ItemType.Consumable)
            {
                if (((ConsumableItem)items[i].item))
                {
                    items[targetSlot].item = items[i].item;
                    items[targetSlot].stacks = items[i].stacks;
                    items[i].item = null;
                    break;
                }
                else
                {
                    Debug.Log(targetSlot.ToString() + " Not Added");
                }
            }
        }

        RefreshInventory();
    }*/

    public void SwapItems(GameObject item1, GameObject item2)
    {
        // Swapping UI elements by sibling index
        int item1Index = item1.transform.GetSiblingIndex();
        int item2Index = item2.transform.GetSiblingIndex();

        item1.transform.SetSiblingIndex(item2Index);
        item2.transform.SetSiblingIndex(item1Index);

        // Log for debugging
        Debug.Log($"Swapped positions of {item1.name} and {item2.name} in the UI.");

        // Assuming we also need to swap data
        /*SwapItemData(item1.GetComponent<Controller_ItemSlot>(), item2.GetComponent<Controller_ItemSlot>());*/
    }

    // Swap the data associated with the items
    private void SwapItemData(Controller_ItemSlot itemSlot1, Controller_ItemSlot itemSlot2)
    {
        Controller_ItemSlot tempItem = new Controller_ItemSlot(itemSlot1.item, itemSlot1.stacks);
        itemSlot1.item = itemSlot2.item;
        itemSlot2.item = tempItem;

        // You might need to swap more properties depending on your Controller_ItemSlot setup
    }

    // Example method to call when an item is selected to be swapped
    public void OnItemClicked(GameObject item)
    {
        if (selectedItem == null)
        {
            selectedItem = item; // First item selected
        }
        else
        {
            // Swap with the previously selected item
            SwapItems(selectedItem, item);
            selectedItem = null; // Reset selection after swap
        }
    }

    public void SetItemSelected(GameObject item)
    {
        selectedItem = item; // First item selected
    }

    public void OnItemDrop(GameObject item)
    {
        // Swap with the previously selected item
        SwapItems(selectedItem, item);
        selectedItem = null; // Reset selection after swap
    }

    public void SaveInventory()
    {
        InventoryData inventoryData = new InventoryData();
        foreach (var slot in items)
        {
            inventoryData.itemSlots.Add(new ItemSlotData(slot));
        }

        string json = JsonUtility.ToJson(inventoryData, true);
        File.WriteAllText(Application.persistentDataPath + "/inventory.json", json);
    }

    public void LoadInventory()
    {
        string path = Application.persistentDataPath + "/inventory.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            InventoryData inventoryData = JsonUtility.FromJson<InventoryData>(json);

            items.Clear();
            foreach (var slotData in inventoryData.itemSlots)
            {
                BaseItem item = itemDictionary.GetValueByKey(slotData.itemName.ToString());
                items.Add(new Controller_ItemSlot(item, slotData.stacks));
            }

            RefreshInventory();
        }
    }

    public void OpenInventory()
    {
        CanvasGroup inventUIGroupe = inventPanel.GetComponent<CanvasGroup>();
        inventUIGroupe.alpha = 1.0f; // Make UI element visible
        inventUIGroupe.interactable = true; // Enable interaction
        inventUIGroupe.blocksRaycasts = true; // Enable raycasting
    }
    public void CloseInventory()
    {
        SaveInventory();
        CanvasGroup inventUIGroupe = inventPanel.GetComponent<CanvasGroup>();
        inventUIGroupe.alpha = 0f; // Make UI element invisible
        inventUIGroupe.interactable = false; // Disable interaction
        inventUIGroupe.blocksRaycasts = false; // Disable raycasting
    }
}

public enum InvenType
{
    Inventory,
    ItemPouch
}

public enum ItemSlotType
{
    Inventory,
    Consumable
}

[System.Serializable]
public class ItemSlotData
{
    public string itemName;
    public int stacks;

    public ItemSlotData(Controller_ItemSlot slot)
    {
        itemName = slot.item?.itemName ?? "";
        stacks = slot.stacks;
    }
}

[System.Serializable]
public class InventoryData
{
    public List<ItemSlotData> itemSlots = new List<ItemSlotData>();
}
