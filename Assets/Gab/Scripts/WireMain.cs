using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireMain : MonoBehaviour
{

    [SerializeField] private LightSwitch _lightSwitch;
    
    static public WireMain Instance;
    
    public int switchCount;
    public GameObject winText;
    private int onCount = 0;
    


    private void Awake()
    {
        Instance = this;
    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            this.transform.parent.gameObject.SetActive(false);
        }
    }

    public void SwitchChange(int points)
    {
        onCount = onCount + points;

        if (onCount == switchCount)
        {
            _lightSwitch.FuseboxOn = true;
            this.transform.parent.gameObject.SetActive(false);
            //winText.SetActive(true);
        }
    }
}
