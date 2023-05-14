using System;
using System.Collections;
using System.Collections.Generic;
using Gab.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class GabPortrait : MonoBehaviour
{
    [SerializeField] private Image portrait1;
    [SerializeField] private Image portrait2;
    [SerializeField] private string name1;
    [SerializeField] private string name2;
    // Start is called before the first frame update

    

    void Start()
    {
        GabManager.OnVariableChanged += ChangePortrait;
    }

    void ChangePortrait(string variableName, int value)
    {
        Debug.Log("VARIABLECHAGNE " + variableName + " " + value);

        if (variableName == name1)
        {
            Debug.Log("Variable Name is name 1 " + variableName);

            if (value == 1)
                portrait1.color = new Color(1, 1, 1, 1);

            if (value == 0)
                portrait1.color = new Color(1, 1, 1, 0);
        }

        if (variableName == name2)

        {

            if (value == 1)
                portrait2.color = new Color(1, 1, 1, 1);

            if (value == 0)
                portrait2.color = new Color(1, 1, 1, 0);

        }
    }
    
}
