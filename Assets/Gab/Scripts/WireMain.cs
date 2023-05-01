using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireMain : MonoBehaviour
{

    static public WireMain Instance;
    
    public int switchCount;
    public GameObject winText;
    private int onCount = 0;


    private void Awake()
    {
        Instance = this;
    }

    public void SwitchChange(int points)
    {
        onCount = onCount + points;

        if (onCount == switchCount)
        {
            winText.SetActive(true);
        }
    }
}
