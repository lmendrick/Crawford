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
    /*
    public float speed = 5f;
    public Animator animator;
    private Vector2 movement;
    private bool _FacingRight = true;
    */
    
    private Inventory inventory;
    private bool InvIsOpen;


    protected void Awake()
    {
        inventory = new Inventory(UseItem);
        _uiInventory.SetInventory(inventory);
        input = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();

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

    public Inventory getInventory()
    {
        return this.inventory;
    }

    private void Update()
    {

        //movement.y = Input.GetAxisRaw("Horizontal");
        //movement.x = Input.GetAxisRaw("Vertical");
        
        //animator.SetFloat("xvelocity", movement.sqrMagnitude);

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

        /*_rigidbody.MovePosition(_rigidbody.position + movement * speed * Time.fixedDeltaTime);
        
         ((Proper flipping in PlayerControllerTest)
         if (movement.x > 0 && !_FacingRight)
        {
            Flip();
        }

        else if (movement.x < 0 && _FacingRight)
        {
            Flip();
        }*/

    }
    
    /*private void Flip()
    {
        
        ((Proper flipping in PlayerControllerTest))
        
        // Switch the way the player is labelled as facing.
        _FacingRight = !_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        
        
    }*/

}
