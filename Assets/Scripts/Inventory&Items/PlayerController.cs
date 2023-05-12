using System;
using System.Collections;
using System.Collections.Generic;
using Gab.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private UI_Inventory _uiInventory;
    [SerializeField] private GameObject InvGUI;
    [SerializeField] private GameObject ItemOverlay;
    private PlayerInput input;
    private Rigidbody2D _rigidbody;

    private SceneManager _sceneManager;

    [SerializeField] private GabConversationSo _conversation;

    
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
        input = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody2D>();
        ItemOverlay.SetActive(false);
        
        //ItemWorld.SpawnItemWorld(new Vector3(20, 20), new Item {itemType = Item.ItemType.Item1, amount =1});
        //animator = GetComponent<Animator>();
        

    }

    void Start()
    {
        _uiInventory.SetInventory(inventory);
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            GabManager.StartNew(_conversation);
            Invoke(nameof(EndConvo), 5);
        }
    }


    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.Key:
                ItemOverlay.SetActive(true);
                //inventory.RemoveItem(new Item { itemType = Item.ItemType.Key, amount = 1 });
                break;
            case Item.ItemType.puzzleP1:
                ItemOverlay.SetActive(true);
                //inventory.RemoveItem(new Item { itemType = Item.ItemType.puzzleP1, amount = 1 });
                break;
            case Item.ItemType.puzzleP2:
                ItemOverlay.SetActive(true);
                //inventory.RemoveItem(new Item { itemType = Item.ItemType.puzzleP2, amount = 1 });
                break;
            case Item.ItemType.puzzleP3:
                ItemOverlay.SetActive(true);
                //inventory.RemoveItem(new Item { itemType = Item.ItemType.puzzleP3, amount = 1 });
                break;
            case Item.ItemType.puzzleP4:
                ItemOverlay.SetActive(true);
                break;
            case Item.ItemType.Crowbar:
                ItemOverlay.SetActive(true);
                //inventory.RemoveItem(new Item { itemType = Item.ItemType.Item1, amount = 1 });
                break;
            case Item.ItemType.Book1:
                ItemOverlay.SetActive(true);
                //Just A Typical book
                break;
            case Item.ItemType.Book2:
                ItemOverlay.SetActive(true);
                //Just A Typical book
                break;
            case Item.ItemType.BookWithKey:
                ItemOverlay.SetActive(true);
                //You Found A Key
                inventory.AddItem(new Item { itemType = Item.ItemType.Key, amount = 1 });
                //ItemWorld.SpawnItemWorld(transform.position, new Item { itemType = Item.ItemType.Key, amount = 1 });
                break;

        }
        
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
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

    private void EndConvo()
    {
        GabManager.End();
    }

}
