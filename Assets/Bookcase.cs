using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bookcase : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    private bool hasCrowbar;
    private bool lightIsOn;
    
    
    

    public string InteractionPrompt => _prompt;
    // Start is called before the first frame update

    public bool Interact(Interactor interactor)
    {
        transform.position = Vector3.left * 2;
        if (hasCrowbar && lightIsOn)
        {
            transform.position = Vector3.left * 2;
        }
        return true;
    }
}
