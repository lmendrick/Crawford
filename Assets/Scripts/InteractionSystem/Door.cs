using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gab.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    private Rigidbody2D _rigidbody;
    
    public string InteractionPrompt => _prompt;

    public Vector2 InteractionPosition => _rigidbody.position;

    [SerializeField] private GameObject _doorSprite;
    [SerializeField] private GameObject _doorBarrier;

    [SerializeField] private GabConversationSo _conversation;
    
    
    

    private Inventory _inventory;
    
    
    public bool Interact(Interactor interactor)
    {
        //Checks to see if the interactor has an inventory
        _inventory = interactor.getInventory();
        //var inventory1 = 
        

        //Checks to see if the inventory contains a key and displays a message if it does
        foreach (Item item in _inventory.GetItemList())  
        {
            if (item.itemType == Item.ItemType.Key)
            {
                Debug.Log("Opening Door!");
                _doorSprite.SetActive(false);
                _doorBarrier.SetActive(false);
                Destroy(gameObject);
                return true;
            }
        }
        
        
        // if (_inventory.GetItemList().Contains(Key));
        // {
        //     Debug.Log("Opening Door!");
        //     return true;
        // }
        
        // If the inventory does not contain a key, displays a message saying so
        Debug.Log("No key found!");
        GabManager.StartNew(_conversation);
        Invoke(nameof(EndConvo), 2);
        return false;
    }

    private void EndConvo()
    {
        GabManager.End();
    }
}
