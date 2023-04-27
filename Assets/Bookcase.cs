using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bookcase : MonoBehaviour
{
    [SerializeField] private string _prompt;

    private bool hasCrowbar;
    private bool lightIsOn;
    

    public string InteractionPrompt => _prompt;
    // Start is called before the first frame update

    public bool Interact(Interactor interactor)
    {
        if (hasCrowbar && lightIsOn)
        {
            
        }
        return true;
    }
}
