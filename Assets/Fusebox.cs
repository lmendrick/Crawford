using System;
using System.Collections;
using System.Collections.Generic;
using Gab.Scripts;
using Unity.VisualScripting;
using UnityEngine;

public class Fusebox : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private GameObject _wirePuzzle;
    [SerializeField] private LightSwitch _lightSwitch;
    [SerializeField] private GabConversationSo _conversation;


    private Inventory _inventory;

    public string InteractionPrompt => _prompt;

    // Start is called before the first frame update
    private void Awake()
    {
        _wirePuzzle.SetActive(false);
    }

    public bool Interact(Interactor interactor)
    {

        _inventory = interactor.getInventory();
        
        //GabManager.StartNew(_conversation);

        foreach (Item item in _inventory.GetItemList())
        {
            if (item.itemType == Item.ItemType.Crowbar)
            {
                _wirePuzzle.SetActive(true);
                return true;


            }

            

        }
        GabManager.StartNew(_conversation);
        Invoke(nameof(CallGabEnd), 2);

        return false;
    }
    
    private void CallGabEnd()
    {
        GabManager.End();
    }

}