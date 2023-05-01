using System;
using System.Collections;
using System.Collections.Generic;
using Gab.Scripts;
using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{

    [SerializeField] private string _prompt;
    [SerializeField] private GameObject light;
    [SerializeField] private GameObject _bookcase;
    [SerializeField] private GabConversationSo _conversation;
    
    
    
    
    public bool FuseboxOn;
    private bool lightIsOn;

    public string InteractionPrompt => _prompt;
    // Start is called before the first frame update
    private void Awake()
    {
        light.SetActive(false);
        _bookcase.SetActive(false);
    }

   

    public bool Interact(Interactor interactor)
    {
        if (lightIsOn)
        {
            lightIsOn = false;
            light.SetActive(false);
            _bookcase.SetActive(false);
            
        }
        else if(!lightIsOn && FuseboxOn)
        {
            lightIsOn = true;
            light.SetActive(true);
            _bookcase.SetActive(true);
        }
        else
        {
            GabManager.StartNew(_conversation);
            Invoke(nameof(CallGabEnd), 3);
        }
        return true;
    }

    public bool GetLightIsOn()
    {
        return lightIsOn;
    }
    
    private void CallGabEnd()
    {
        GabManager.End();
    }
}


