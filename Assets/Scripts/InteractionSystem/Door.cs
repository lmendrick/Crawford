using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    
    public string InteractionPrompt => _prompt;
    
    
    public bool Interact(Interactor interactor)
    {
        //Checks to see if the interactor has an inventory
        var inventory = interactor.GetComponent<InventoryInteract>();

        if (inventory == null) return false;

        //Checks to see if the inventory contains a key and displays a message if it does
        if (inventory.HasKey)
        {
            Debug.Log("Opening Door!");
            return true;
        }
        
        // If the inventory does not contain a key, displays a message saying so
        Debug.Log("No key found!");
        return false;
    }
}
