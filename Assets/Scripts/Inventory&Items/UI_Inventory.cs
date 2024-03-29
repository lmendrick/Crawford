using System;
using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] private ItemOverlay _itemOverlay;
    
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    
    private bool isOpen = false;

    private void Awake()
    {
        
        gameObject.SetActive(false);
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
        
    }
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
       
    }

    private void RefreshInventoryItems()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
      
        
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 70f;
        foreach (Item item in inventory.GetItemList())
        {

            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                _itemOverlay.setItem(item);
                inventory.UseItem(item);
                
            };

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            /*TextMeshProUGUI uiText = itemSlotRectTransform.Find("text").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
                
            }
            else
            {
                uiText.SetText("");
            }*/
            
            x++;
            if (x > 3)
            {
                x = 0;
                y--;
            }
        }

        void invUseItem(Item item)
        {
            inventory.UseItem(item);
        }
    }

    public Inventory getInventory()
    {
        return this.inventory;
    }

    
}
