using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Bookcase : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    private bool hasCrowbar;
    private bool lightIsOn;
    private Transform _position;
    
    private Inventory _inventory;
    
    

    public string InteractionPrompt => _prompt;
    // Start is called before the first frame update

    public bool Interact(Interactor interactor)
    {
        
        _inventory = interactor.getInventory();
        
        foreach (Item item in _inventory.GetItemList())  
        {
            if (item.itemType == Item.ItemType.Crowbar)
            {
                Debug.Log("Opening Door!");
                Destroy(gameObject);
                hasCrowbar.Equals(true);
                return true;
            }
        }
        
        transform.position += Vector3.left * 2;
        if (hasCrowbar && lightIsOn)
        {
            transform.position += Vector3.left * 2;
        }
        return true;
    }
}
