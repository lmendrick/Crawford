using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordKeypad : MonoBehaviour, IInteractable 
{
[SerializeField] private string _prompt;
[SerializeField] private Password _password;


    
    
public string InteractionPrompt => _prompt;

public bool Interact(Interactor interactor)
{
    _password.gameObject.SetActive(true);
    //_password.display();
    return true;
}

}
