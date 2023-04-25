using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Keypad : MonoBehaviour
{

    public TMP_InputField charHolder;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    public GameObject button6;
    public GameObject button7;
    public GameObject button8;
    public GameObject button9;
    public GameObject enterButton;
    public GameObject clearButton;

    [SerializeField] private GameObject keypadDoor;
    
    
    public bool correctCode = false;


    private void Awake()
    {
        charHolder.text = "";
    }



    public void b1()
    {
        charHolder.text += "1";
    }
    
    public void b2()
    {
        charHolder.text += "2";
    }
    
    public void b3()
    {
        charHolder.text += "3";
    }
    
    public void b4()
    {
        charHolder.text += "4";
    }
    
    public void b5()
    {
        charHolder.text += "5";
    }
    
    public void b6()
    {
        charHolder.text += "6";
    }
    
    public void b7()
    {
        charHolder.text += "7";
    }
    
    public void b8()
    {
        charHolder.text += "8";
    }
    
    public void b9()
    {
        charHolder.text += "9";
    }

    public void clearEvent()
    {
        charHolder.text = null;
    }

    public void enterEvent()
    {
        if (charHolder.text == "1234")
        {
            Debug.Log("Success!");
            correctCode = true;
            Destroy(keypadDoor);
        }
        else
        {
            Debug.Log("Failed");
        }
    }


}
