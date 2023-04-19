using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private UI_Inventory _uiInventory;
    [SerializeField] private GameObject InvGUI;
    private PlayerInput input;
    private Inventory inventory;
    private bool InvIsOpen;
    private void Awake()
    {
        inventory = new Inventory(UseItem);
        _uiInventory.SetInventory(inventory);
        input = GetComponent<PlayerInput>();

    }

    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.Item1:
                Debug.Log("Used Item 1");
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Item1, amount = 1 });
                break;
        }
        
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        inventory.AddItem(itemWorld.GetItem());
        itemWorld.DestroySelf();
    }

    private void Update()
    {
        if (input.actions["Inventory"].WasPressedThisFrame())
        {
            Debug.Log("Inv");
            if (!InvIsOpen)
            {
                InvGUI.SetActive(true);
                InvIsOpen = true;
            }
            else
            {
                InvGUI.SetActive(false);
                InvIsOpen = false;
            }
        }
    }
}
