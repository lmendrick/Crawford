using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{

    // private Camera _mainCam;      //ONLY USED FOR 3D
    [SerializeField] GameObject _uiPanel;
    [SerializeField] TextMeshProUGUI _promptText;
    

    private void Start()
    {
        // _mainCam = Camera.main;   //ONLY USED FOR 3D
        _uiPanel.SetActive(false);
    }



    //ONLY USED FOR 3D
    private void LateUpdate()
    {
        // //ONLY USED FOR 3D
        // var rotation = _mainCam.transform.rotation;
        // transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);



    }

    public bool IsDisplayed = false;
    
    public void SetUp (string promptText)
    {
        _promptText.text = promptText;
        _uiPanel.SetActive(true);
        IsDisplayed = true;
    }

    public void Close()
    {
        _uiPanel.SetActive(false);
        IsDisplayed = false;
    }
    
}
