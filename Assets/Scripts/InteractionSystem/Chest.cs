using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{

    [SerializeField] private string _prompt;


    
    public string InteractionPrompt => _prompt;


    
    private bool hasBeenOpened= false;
    
    


    
public bool Interact(Interactor interactor)
{
    if (!hasBeenOpened){
        ItemWorld.SpawnItemWorld(transform.position, new Item { itemType = Item.ItemType.Key, amount =1});
        hasBeenOpened = true;
        return true;
        
        
         /*inventory.AddItem();*/
            Debug.Log("Opening Chest!");                    //Add interaction here, could get inventory from interactor to check for keys etc (see door script)
              
            // inventory.AddItem(new Item { itemType = Item.ItemType.Key, amount =1});
            Debug.Log("You found a key!");                    //Add interaction here, could get inventory from interactor to check for keys etc (see door script)
                // if (itemSpawn == null) return false;
    }
        
   
        
        
        
    
        
    /*inventory.AddItem();*/
    Debug.Log("Opening Chest!");                    //Add interaction here, could get inventory from interactor to check for keys etc (see door script)
      
    // inventory.AddItem(new Item { itemType = Item.ItemType.Key, amount =1});
    Debug.Log("You found a key!");                    //Add interaction here, could get inventory from interactor to check for keys etc (see door script)
        
    return false;
    }
}
