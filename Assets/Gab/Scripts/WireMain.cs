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

    public AudioSource soundOnSFX;
    


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
            soundOnSFX.Play();
            Invoke(nameof(End), 2);
            //winText.SetActive(true);
        }
    }

    private void End()
    {
        this.transform.parent.gameObject.SetActive(false);
    }
}

