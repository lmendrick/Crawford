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
    private Rigidbody2D _rigidbody;
    private Inventory inventory;
    private bool InvIsOpen;

    
    private void Awake()
    {
        inventory = new Inventory(UseItem);
        _uiInventory.SetInventory(inventory);
        input = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody2D>();

        ItemWorld.SpawnItemWorld(new Vector3(5, 5), new Item { itemType = Item.ItemType.Item1, amount = 1 });

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
        Debug.Log("Item Added");
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

    private void FixedUpdate()
    {
        //set direction to the Move action's Vector2 value
        var dir = input.actions["Move"].ReadValue<Vector2>();

        //change the velocity to match the Move (every physics update)
        _rigidbody.velocity = dir * 5;
    }
}
