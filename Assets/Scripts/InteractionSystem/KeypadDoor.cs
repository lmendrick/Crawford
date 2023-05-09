using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class KeypadDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    
    
    public string InteractionPrompt => _prompt;

    [SerializeField] private GameObject _keypad;

    private bool _keypadEnabled = false;
    

    private Inventory _inventory;
    
    private String newCode = "";

    

    private void Awake()
    {
        
        Random rnd = new Random();
        String code = (rnd.Next(1000, 9999).ToString());
        char replaceNum = (char)(rnd.Next(49, 57));
        newCode = code.Replace('0', replaceNum);
        Debug.Log(newCode);
        
    }
    
    
    public bool Interact(Interactor interactor)
    {
        //Checks to see if the interactor has an inventory
        _inventory = interactor.getInventory();
        //var inventory1 = 
        

        //Checks to see if the inventory contains a key and displays a message if it does
        _keypad.SetActive(true);
        _keypadEnabled = true;
        
        
        // if (_inventory.GetItemList().Contains(Key));
        // {
        //     Debug.Log("Opening Door!");
        //     return true;
        // }
        
        // If the inventory does not contain a key, displays a message saying so
        Debug.Log("No key found!");
        return false;
    }

    private void FixedUpdate()
    {
        if (_keypadEnabled)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                _keypad.SetActive(false);
                _keypadEnabled = false;
            }
            

        }

        
    }
    

    public String GetCode()
    {
        return newCode;
    }
    
}
