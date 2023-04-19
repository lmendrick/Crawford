using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{

    [SerializeField] private string _prompt;
    
    public string InteractionPrompt => _prompt;
    
    
    public bool Interact(Interactor interactor)
    {

        ItemWorld.SpawnItemWorld(transform.position, new Item { itemType = Item.ItemType.Key, amount =1});
        
       // if (itemSpawn == null) return false;
        
       // inventory.AddItem(new Item { itemType = Item.ItemType.Key, amount =1});
        Debug.Log("You found a key!");                    //Add interaction here, could get inventory from interactor to check for keys etc (see door script)
        return true;
    }
}
