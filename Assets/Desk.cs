using System.Collections;
using System.Collections.Generic;
using Gab.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Desk : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    
    [SerializeField] private GabConversationSo _conversation;

    [SerializeField] private bool _hasItem;

    [SerializeField] private AudioSource _openSFX;

    [SerializeField] private GameObject _deskNoteCanvas;
    
  
    
    
    

    public Item item;
    
    
    public string InteractionPrompt => _prompt;


    
    private bool hasBeenOpened= false;







    public bool Interact(Interactor interactor)
    {
        _deskNoteCanvas.SetActive(true);
        _openSFX.Play();
        return true;
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
