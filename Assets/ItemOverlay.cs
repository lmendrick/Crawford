using System;
using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemOverlay : MonoBehaviour
{
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
}
