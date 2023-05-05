using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Bookcase : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private LightSwitch _light;
    [SerializeField] private GameObject _sprite;
    [SerializeField] private QT_Event pushBookcase;
    [SerializeField] private GameObject progressBar;
    [SerializeField] private GameObject _wallSprite;
    [SerializeField] private GameObject _sceneChanger;
    
    
    
    
    

    private bool hasCrowbar;
    private bool lightIsOn;
    private Transform _position;
    
    private Inventory _inventory;
    
    
    public string InteractionPrompt => _prompt;

    private void Awake()
    {
        progressBar.SetActive(false);
        _sceneChanger.SetActive(false);
    }

    public bool Interact(Interactor interactor)
    {
        
        _inventory = interactor.getInventory();
        
        foreach (Item item in _inventory.GetItemList())  
        {
            if (item.itemType == Item.ItemType.Crowbar && _light.GetLightIsOn())
            {
                progressBar.SetActive(true);
              
                return true;
                
                
            }
        }
        
        // transform.position += Vector3.left * 2;
        // if (hasCrowbar && lightIsOn)
        // {
        //     transform.position += Vector3.left * 2;
        // }
        return true;
    }
    void Update()
    {
        if (pushBookcase.eventSuccess)
        {
            _sprite.transform.position += Vector3.left * 3;
            progressBar.SetActive(false);
            _sprite.GetComponent<SpriteRenderer>().sortingOrder = 6;
            //_wallSprite.GetComponent<SpriteRenderer>().sortingOrder = 5;
            _sceneChanger.SetActive(true);
            gameObject.SetActive(false);
        }
    }

   

}
