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
        RectTransform rectTransform = text.GetComponent<RectTransform>();
        item = new Item { itemType = i.itemType, amount = 1 };
        image.sprite = item.GetPopUpSprite();
        if (item.itemType == Item.ItemType.puzzleP1)
        {
            Char num = (_keypad.GetCode())[0];
            //text.SetText("April " +  num.ToString());
            text.SetText( num.ToString());
        }
        else if (item.itemType == Item.ItemType.puzzleP2)
        {
            Char num = (_keypad.GetCode())[1];
            //text.SetText("Around " + num.ToString() +
             //            " o'clock, the boys came in with some detective fella. Apparently the boss wanted him for some questioning, stuck his snout where it shouldn’t’ have been.");
            text.SetText( num.ToString());
        }
        else if (item.itemType == Item.ItemType.puzzleP3)
        {
            Char num = (_keypad.GetCode())[2];
            //text.SetText("He looked like a tall fella, maybe around six feet " + num.ToString() +
              //           " tall. You don’t see komodos around here often, didn’t think they’d be that tall.");
            text.SetText( num.ToString());
        }
        else if (item.itemType == Item.ItemType.puzzleP4)
        {
            Char num = (_keypad.GetCode())[3];
            text.SetText( num.ToString());
            //text.SetText("He was a pain to get through that door behind the shelf, kept trying to fight his way out. Fortunately we got him to settle down enough to cooperate. Who would’ve thought some guy who looked to be around sixty " + num.ToString() + " still had so much vigor.  ");
        }


    }
}
