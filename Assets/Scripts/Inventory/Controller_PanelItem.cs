using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Controller_PanelItem : MonoBehaviour
{
    public Controller_InventoryManager inventoryManager;
    public Controller_TargetItem itemTarget;

    public Image panelImage;
    public Image itemImage;
    public Controller_ItemSlot itemSlot;
    public Controller_ItemSlot itemSelected;
    public TMP_Text itemText;
    public TextMeshProUGUI quantityText;
    public ItemType slotType;

    public GameObject itemTargetPanel;
    private GameObject targetPanel = null;

    /*private bool click;*/
    public MouseItem mouseItem = new MouseItem();
    public ControllerMouse mouse;
    private int swapTargetIndex;

    /*public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (mouse.itemSlot.item == null)
            {
                if (itemSlot.item != null)
                {
                    itemSlot.item.UseItem(characterData);
                    Debug.Log("Item used");
                }
            }
            else if (mouse.itemSlot.item != null)
            {
                *//*OnAction();*//*
            }
        }
        *//*else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (mouse.itemSlot.item != null)
            {
                
            }
        }*//*
    }*/

    private void Start()
    {
        var obj = this.gameObject;
        AddEvent(obj, EventTriggerType.PointerClick, delegate { OnClick(obj); });
        AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
        AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
        AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
        AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
        AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
    }

    public void OnClick(GameObject obj)
    {
        /*if (itemTargetPanel.activeSelf)
        {
            itemTargetPanel.SetActive(false);
        }*/

        if (itemSlot.item != null)
        {
            /*itemTargetPanel.SetActive(true);*/
            itemTarget.OpenTargetPanel();
            itemTarget.SetItemSelected(itemSlot);
        }

        inventoryManager.RefreshInventory();
    }

    public void OnEnter(GameObject obj)
    {
        /*if(obj.GetComponent<Controller_PanelItem>().itemSlot == mouseItem.itemSlot) {
            return;
        }*/

        /*if (!mouseItem.isDragging)
        {
            inventoryManager.OnItemClicked(obj);
        }*/

        /*
        var objectItem = obj;

        if (mouseItem.hoverObj && mouseItem.hoverObj == objectItem)
        {
            return;
        }*/

        /*swapTargetIndex = obj.transform.GetSiblingIndex();*/

        inventoryManager.SetItemSelected(obj);

        mouseItem.hoverObj = obj;
        Debug.Log("hover" + obj);

        mouseItem.itemSlot = obj.GetComponent<Controller_PanelItem>().itemSlot;
        mouseItem.hoverItem = obj.GetComponent<Controller_PanelItem>().itemSlot.item;

        targetPanel = obj; // Set targetPanel to the panel being targeted

        /*var slotTarget = mouseItem.itemSlot;*/

        Debug.Log(mouseItem.itemSlot.item);
        Debug.Log(mouseItem.hoverObj);
    }

    public void OnExit(GameObject obj)
    {
        if (mouseItem.isDragging)
        {
            mouseItem.hoverObj = null;
            mouseItem.hoverItem = null;
            Debug.Log("exit" + mouseItem.hoverObj);
        }
        else
        {
            
        }
    }

    public void OnDragStart(GameObject obj)
    {
        mouseItem.itemSlot = null;

        var mouseObject = new GameObject();
        mouseObject.transform.SetParent(transform.parent.parent.parent);
        var rt = mouseObject.AddComponent<RectTransform>();
        rt.position = Input.mousePosition;
        rt.localScale = new Vector3(1, 1, 1);
        rt.position = Input.mousePosition;
        if (itemSlot.item != null)
        {
            var img = mouseObject.AddComponent<Image>();
            img.sprite = itemSlot.item.itemIcon;
            img.raycastTarget = false;
        }
        mouseItem.obj = mouseObject;
        mouseItem.item = itemSlot.item;

        mouseItem.isDragging = true;
        
    }

    public void OnDragEnd(GameObject obj)
    {
        mouseItem.isDragging = false;
        /*Debug.Log(itemSlot.item);
        *//*Debug.Log(mouseItem.hoverObj);
        Debug.Log(mouseItem.hoverItem);
        Debug.Log(mouseItem.itemSlot);*//*
        Debug.Log(mouseItem.hoverObj);

        Debug.Log(targetPanel);
        Debug.Log(targetPanel.GetComponent<Controller_PanelItem>().itemSlot.item);

        if (mouseItem.hoverObj != null)
        {
            *//*if(itemSlot == null || mouseItem.itemSlot == null || !mouseItem.hoverObj)
            {
                Debug.Log("One of the item slots is null");
            }
            else
            {
                Debug.Log(itemSlot.item);
                Debug.Log(mouseItem.itemSlot.item);
                SwapItem(itemSlot, mouseItem.hoverObj.GetComponent<Controller_ItemSlot>());
            }*//*

            Debug.Log(itemSlot.item);
            Debug.Log(mouseItem.hoverObj);
            SwapItem(itemSlot, mouseItem.hoverObj.GetComponent<Controller_PanelItem>().itemSlot);

            inventoryManager.RefreshInventory();

            
        }
        else if (mouseItem.hoverObj == null)
        {
            Debug.Log("Gagal");
        }*/

        /*int swapSourceIndex = mouseItem.hoverObj.transform.GetSiblingIndex();*/

        /*obj.transform.SetSiblingIndex(swapTargetIndex);*/

        inventoryManager.OnItemDrop(obj);

        Destroy(mouseItem.obj);
        mouseItem.item = null;
    }

    public void OnDrag(GameObject obj)
    {
        if (mouseItem.obj != null)
            /*mouseItem.obj.GetComponent<Image>().gameObject.SetActive(true);*/
            mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
        // Update mouseItem.hoverObj based on the object currently under the mouse cursor

       /* RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.CompareTag("Panel"))
            {
                mouseItem.hoverObj = hitObject;
            }
            else
            {
                mouseItem.hoverObj = null;
            }
        }
        else
        {
            mouseItem.hoverObj = null;
        }*/
    }

    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    public void SwapItem(Controller_ItemSlot itemSlot1, Controller_ItemSlot itemSlot2)
    {
        /*Debug.Log(itemSlot1.item);
        Debug.Log(itemSlot2.item);*/

        if(itemSlot2 != null)
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

    public void OnAction()
    {
        /*if (inventoryManager != null)
        {
            mouse = inventoryManager.mouse;

            //Grab item if mouse slot is empty
            if (mouse.itemSlot.item == null)
            {
                if (itemSlot.item != null)
                {
                    *//*PickupItem();
                    FadeOut();*//*
                }
            }

            else
            {
                *//*if (slotType == ItemSlotType.Inventory)
                {
                    OnActionInventory();
                }
                else if (slotType == ItemSlotType.Consumable)
                {
                    OnActionConsumableItem();
                }
                else
                {
                    OnActionEquipmentSlot();
                }*//*
                

            }
            OnActionInventory();
        }*/
        OnActionInventory();
    }

    public void OnActionInventory()
    {
        /*//Clicked on original slot
        if (itemSlot == mouse.itemSlot)
        {
            mouse.EmptySlot();
        }
        //Clicked on empty slot
        else if (itemSlot.item == null)
        {
            
        }
        else if (itemSlot.item != mouse.itemSlot.item)
        {
            SwapItem(itemSlot, mouse.itemSlot);
        }
        //Clicked on occupied slot of different item type
        else if (itemSlot.item.maxQuantity != mouse.itemSlot.item.maxQuantity)
        {
            SwapItem(itemSlot, mouse.itemSlot);
        }
        //Clicked on occupided slot of same type
        else if (itemSlot.stacks < itemSlot.item.maxQuantity)
        {
            
        }
        else if (itemSlot.stacks >= itemSlot.item.maxQuantity)
        {
            SwapItem(itemSlot, mouse.itemSlot);
        }
        else
        {
            mouse.EmptySlot();
        }*/

        /*SwapItem(itemSlot, mouseItem.obj.GetComponent<Controller_PanelItem>().itemSlot);*/
        SwapItem(itemSlot, mouseItem.itemSlot);

        inventoryManager.RefreshInventory();

    }

    

}

public class MouseItem
{
    public GameObject obj;
    public BaseItem item;
    public Controller_ItemSlot itemSlot;
    public Controller_PanelItem itemPanel;
    public BaseItem hoverItem;
    public GameObject hoverObj;
    public bool isDragging = false;
}
