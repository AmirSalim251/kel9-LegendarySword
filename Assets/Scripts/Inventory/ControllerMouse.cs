using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControllerMouse : MonoBehaviour
{
    public GameObject mouseItemUI;
    public Image mouseCursor;
    public Controller_ItemSlot itemSlot;
    public Image itemImage;
    public TextMeshProUGUI quantityText;
    public GameObject obj;
    public BaseItem item;
    public Controller_PanelItem itemPanel;
    public BaseItem hoverItem;
    public GameObject hoverObj;

    public bool isDragging = false;

    public Controller_PanelItem sourceItemPanel;
    public int splitSize;

    private void Start()
    {
    }
    void Update()
    {
        transform.position = Input.mousePosition;
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            mouseCursor.enabled = false;
            mouseItemUI.SetActive(false);
        }
        else
        {
            mouseCursor.enabled = true;

            if (itemSlot.item != null)
            {
                mouseItemUI.SetActive(true);
            }
            else
            {
                mouseItemUI.SetActive(false);
            }
        }
        if (itemSlot.item != null)
        {
            
            quantityText.text = "" + splitSize;
            if (splitSize == itemSlot.stacks) sourceItemPanel.quantityText.gameObject.SetActive(false);
            else
            {
                sourceItemPanel.quantityText.gameObject.SetActive(true);
                sourceItemPanel.quantityText.text = "" + (itemSlot.stacks - splitSize);
            }
        }
    }

    public void SetUI()
    {
        quantityText.text = "" + splitSize;
        itemImage.sprite = itemSlot.item.itemIcon;
    }

    public void EmptySlot()
    {
        itemSlot = new Controller_ItemSlot(null, 0);
    }
}
