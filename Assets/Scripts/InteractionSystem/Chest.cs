using System.Collections;
using System.Collections.Generic;
using Gab.Scripts;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{

    [SerializeField] private string _prompt;
    
    [SerializeField] private GabConversationSo _conversation;

    [SerializeField] private bool _hasItem;
    

    public Item item;
    
    
    public string InteractionPrompt => _prompt;


    
    private bool hasBeenOpened= false;
    
    
    
    


    
public bool Interact(Interactor interactor)
{
    if (!hasBeenOpened){
        
        GabManager.StartNew(_conversation);

        Invoke(nameof(CallGabEnd), 3);
        
        if (_hasItem)
        {
            ItemWorld.SpawnItemWorld(interactor.transform.position, new Item { itemType = item.itemType, amount = 1 });
            hasBeenOpened = true;

         

            return true;


            /*inventory.AddItem();*/
            Debug.Log("Opening Chest!"); //Add interaction here, could get inventory from interactor to check for keys etc (see door script)

            // inventory.AddItem(new Item { itemType = Item.ItemType.Key, amount =1});
            Debug.Log("You found a key!"); //Add interaction here, could get inventory from interactor to check for keys etc (see door script)
            // if (itemSpawn == null) return false;
        }
    }
        
   
        
        
        
    
        
    /*inventory.AddItem();*/
    Debug.Log("Opening Chest!");                    //Add interaction here, could get inventory from interactor to check for keys etc (see door script)
      
    // inventory.AddItem(new Item { itemType = Item.ItemType.Key, amount =1});
    Debug.Log("You found a key!");                    //Add interaction here, could get inventory from interactor to check for keys etc (see door script)
        
    return false;
    }

private void CallGabEnd()
{
    GabManager.End();
}

}
