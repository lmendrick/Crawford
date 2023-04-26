using System;
using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class ItemOverlay : MonoBehaviour
{
    private Item item;

    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private KeypadDoor _keypad;
    
    
    
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<Button_UI>().ClickFunc = () =>
        {
            exitOverlay();
        };
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            exitOverlay();
        }
    }

    // Update is called once per frame
    void exitOverlay ()
    {
        gameObject.SetActive(false);
    }

    public void setItem(Item i)
    {
        item = new Item { itemType = i.itemType, amount = 1 };
        image.sprite = item.GetPopUpSprite();
        if (item.itemType == Item.ItemType.puzzleP1)
        {
            Char num = (_keypad.GetCode())[0];
            text.SetText(num.ToString());
        }
        else if (item.itemType == Item.ItemType.puzzleP2)
        {
            Char num = (_keypad.GetCode())[1];
            text.SetText(num.ToString());
        }
        else if (item.itemType == Item.ItemType.puzzleP3)
        {
            Char num = (_keypad.GetCode())[2];
            text.SetText(num.ToString());
        }
        else if (item.itemType == Item.ItemType.puzzleP4)
        {
            Char num = (_keypad.GetCode())[3];
            text.SetText(num.ToString());
        }


    }
}
