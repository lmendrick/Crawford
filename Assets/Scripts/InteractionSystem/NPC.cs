using System.Collections;
using System.Collections.Generic;
using Gab.Scripts;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    [SerializeField] private GabConversationSo _conversation;
    
    [SerializeField] private GabConversationSo _conversation2;
    

    public Item item;
    
    
    public string InteractionPrompt => _prompt;


    
    private bool hasBeenTalkedTo = false;
    
    


    
    public bool Interact(Interactor interactor)
    {
        // if (!hasBeenTalkedTo){
            //ItemWorld.SpawnItemWorld(interactor.transform.position, new Item { itemType = item.itemType, amount =1});
            hasBeenTalkedTo = true;
            
            
            GabManager.StartNew(_conversation);
            
            return true;
        
        
            /*inventory.AddItem();*/
            Debug.Log("Opening Chest!");                    //Add interaction here, could get inventory from interactor to check for keys etc (see door script)
              
            // inventory.AddItem(new Item { itemType = Item.ItemType.Key, amount =1});
            Debug.Log("You found a key!");                    //Add interaction here, could get inventory from interactor to check for keys etc (see door script)
            // if (itemSpawn == null) return false;
        //}
        // else
        // {
        //     GabManager.StartNew(_conversation2);
        // }
        
   
        
        
        
    
        
        /*inventory.AddItem();*/
        Debug.Log("Opening Chest!");                    //Add interaction here, could get inventory from interactor to check for keys etc (see door script)
      
        // inventory.AddItem(new Item { itemType = Item.ItemType.Key, amount =1});
        Debug.Log("You found a key!");                    //Add interaction here, could get inventory from interactor to check for keys etc (see door script)
        
        return false;
    }
}
