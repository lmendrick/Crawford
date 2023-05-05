using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GabPortrait : MonoBehaviour
{
    [SerializeField] private GameObject portrait1;
    [SerializeField] private string name1;
    // Start is called before the first frame update
    void OnStart()
    {
        GabManager.OnVariableChanged += ChangePortrait;
    }

    void ChangePortrait(string variableName, int value)
    {
        if (variableName == name1) {
            if (value == 1)
                portrait1.SetActive(true);
            else
                portrait1.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
