using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : PlayerController
{

    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private InteractionPromptUI _interactionPromptUI;
    

    private readonly Collider2D[] _colliders = new Collider2D[3];
    [SerializeField] private int _numFound;


    private IInteractable _interactable;
    
    private PlayerInput _input;

    //private Vector3 _uiOffset = new Vector3()



  
    private void Update()
    {
        //Number of objects on the interactable layer that the player (interaction point) is colliding with
        _numFound = Physics2D.OverlapCircleNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders,
            _interactableMask);

       

        // If the interactor (player) is colliding with an interactable object
        if (_numFound > 0)
        {
            //References the IInteractable script
            _interactable = _colliders[0].GetComponent<IInteractable>();

            if (_interactable != null)          
            {
                // Displays UI prompt if one is not already showing
                if (!_interactionPromptUI.IsDisplayed)
                {
                  
                    _interactionPromptUI.SetUp(_interactable.InteractionPrompt, _colliders[0].transform.position + Vector3.up * 2 );
                }

                if (Keyboard.current.eKey.wasPressedThisFrame)              // Change this to work with Game Input Actions instead of just e key     
                {
                    _interactable.Interact(this);
                }
            }
        }
        else
        {
            //Closes the prompt if the player is no longer colliding with the interactable object
            if (_interactable != null) _interactable = null;
            if (_interactionPromptUI.IsDisplayed) _interactionPromptUI.Close();
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
