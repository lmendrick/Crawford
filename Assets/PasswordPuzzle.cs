using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PasswordPuzzle : MonoBehaviour
{
    public TMP_InputField charHolder1;
    public TMP_InputField charHolder2;
    public TMP_InputField charHolder3;
    public TMP_InputField charHolder4;
    
    public GameObject upButton1;
    public GameObject downButton1;
    public GameObject upButton2;
    public GameObject downButton2;
    public GameObject upButton3;
    public GameObject downButton3;
    public GameObject upButton4;
    public GameObject downButton4;

    private void Awake()
    {
        charHolder1.text = "0";
        charHolder2.text = "0";
        charHolder3.text = "0";
        charHolder4.text = "0";
    }

    public void up1()
    {

        var i = int.Parse(charHolder1.text);

        if (i != 9)
        {
            i++;
            charHolder1.text = i.ToString();
        }
        else
        {
            i = 0;
            charHolder1.text = i.ToString();
        }
        
    }

    public void down1()
    {
        var i = int.Parse(charHolder1.text);

        if (i != 0)
        {
            i--;
            charHolder1.text = i.ToString();
        }
        else
        {
            i = 9;
            charHolder1.text = i.ToString();
        }
    }
    
    public void up2()
    {

        var i = int.Parse(charHolder2.text);

        if (i != 9)
        {
            i++;
            charHolder2.text = i.ToString();
        }
        else
        {
            i = 0;
            charHolder2.text = i.ToString();
        }
        
    }

    public void down2()
    {
        var i = int.Parse(charHolder2.text);

        if (i != 0)
        {
            i--;
            charHolder2.text = i.ToString();
        }
        else
        {
            i = 9;
            charHolder2.text = i.ToString();
        }
    }
    
    public void up3()
    {

        var i = int.Parse(charHolder3.text);

        if (i != 9)
        {
            i++;
            charHolder3.text = i.ToString();
        }
        else
        {
            i = 0;
            charHolder3.text = i.ToString();
        }
        
    }

    public void down3()
    {
        var i = int.Parse(charHolder3.text);

        if (i != 0)
        {
            i--;
            charHolder3.text = i.ToString();
        }
        else
        {
            i = 9;
            charHolder3.text = i.ToString();
        }
    }
    
    public void up4()
    {

        var i = int.Parse(charHolder4.text);

        if (i != 9)
        {
            i++;
            charHolder4.text = i.ToString();
        }
        else
        {
            i = 0;
            charHolder4.text = i.ToString();
        }
        
    }

    public void down4()
    {
        var i = int.Parse(charHolder4.text);

        if (i != 0)
        {
            i--;
            charHolder4.text = i.ToString();
        }
        else
        {
            i = 9;
            charHolder4.text = i.ToString();
        }
    }
}
