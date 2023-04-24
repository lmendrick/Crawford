using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleChecker : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField] private UI_Inventory _uiInventory;
    private bool canCraft = false;
    
    // Start is called before the first frame update
    void Awake()
    {
        inventory = _uiInventory.getInventory();
    }

    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
