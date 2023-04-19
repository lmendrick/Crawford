using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    
    public string InteractionPrompt => _prompt;

    private Inventory _inventory;
    
    
    public bool Interact(Interactor interactor)
    {
        //Checks to see if the interactor has an inventory
        var inventory = interactor.GetComponent<InventoryInteract>();
        //var inventory1 = 

        if (inventory == null) return false;

        //Checks to see if the inventory contains a key and displays a message if it does
         if (inventory.HasKey)
         {
             Debug.Log("Opening Door!");
             return true;
         }
        
        // if (_inventory.GetItemList().Contains(Key));
        // {
        //     Debug.Log("Opening Door!");
        //     return true;
        // }
        
        // If the inventory does not contain a key, displays a message saying so
        Debug.Log("No key found!");
        return false;
    }
}
