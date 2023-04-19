using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject invMenu;
    

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
                if (Input.GetKeyDown(KeyCode.I))
                {
                    invMenu.gameObject.SetActive(!invMenu.gameObject.activeSelf);
                } 
    }
}
