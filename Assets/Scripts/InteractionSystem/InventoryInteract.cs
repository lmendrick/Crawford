using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryInteract : MonoBehaviour
{

    public bool HasKey = false;

    private void Update()
    {
        // Toggles "HasKey" for testing
        if (Keyboard.current.qKey.wasPressedThisFrame) HasKey = !HasKey;

    }
}
