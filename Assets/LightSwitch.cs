using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{

    [SerializeField] private string _prompt;
    [SerializeField] private GameObject light;

    private bool lightIsOn;

    public string InteractionPrompt => _prompt;
    // Start is called before the first frame update

    public bool Interact(Interactor interactor)
    {
        if (lightIsOn)
        {
            lightIsOn = false;
            light.SetActive(false);
        }
        else
        {
            lightIsOn = true;
            light.SetActive(true);
        }
        return true;
    }
}


