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

    private Transform ItemImage;
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<Button_UI>().ClickFunc = () =>
        {
            exitOverlay();
        };
        ItemImage = transform.Find("Image");
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
        RectTransform overlaySlotRectTransform = Instantiate(this).GetComponent<RectTransform>();
        Image image = overlaySlotRectTransform.Find("Image").GetComponent<Image>();
        image.sprite = item.GetSprite();
    
    }
}
