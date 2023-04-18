using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{

    [SerializeField] private string _prompt;
    
    public string InteractionPrompt => _prompt;
    
    
    public bool Interact(Interactor interactor)
    {
        Debug.Log("Opening Chest!");                    //Add interaction here, could get inventory from interactor to check for keys etc (see door script)
        return true;
    }
}
