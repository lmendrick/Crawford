using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fusebox : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private GameObject _wirePuzzle;
    [SerializeField] private LightSwitch _lightSwitch;
    
    
    public string InteractionPrompt => _prompt;
    // Start is called before the first frame update
    private void Awake()
    {
        _wirePuzzle.SetActive(false);
    }

    public bool Interact(Interactor interactor)
    {
        _wirePuzzle.SetActive(true);
        return true;
    }
}