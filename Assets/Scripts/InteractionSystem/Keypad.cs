using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using Random = System.Random;


public class Keypad : MonoBehaviour
{

    public TMP_InputField charHolder;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    public GameObject button6;
    public GameObject button7;
    public GameObject button8;
    public GameObject button9;
    public GameObject enterButton;
    public GameObject clearButton;
    
    public GameObject redLight;
    public GameObject greenLight;
    [FormerlySerializedAs("incorrectCode")] public AudioSource incorrectCodeSFX;
    public AudioSource correctCodeSFX;
    public AudioSource doorSFX;

    [SerializeField] private GameObject _wall;
    


    [SerializeField] private KeypadDoor _keypadDoor;
    [SerializeField] private GameObject sceneChanger;
    

    private String _newCode;

    
    public bool correctCode = false;

    
    

    private void Awake()
    {
        charHolder.text = "";

        //_newCode = _keypadDoor.GetCode();

        // Random rnd = new Random();
        // String code = (rnd.Next(1000, 9999).ToString());
        // char replaceNum = (char)(rnd.Next(49, 57));
        // newCode = code.Replace('0', replaceNum);
        // Debug.Log(newCode);
        
        sceneChanger.SetActive(false);
        redLight.SetActive(false);
        greenLight.SetActive(false);
        incorrectCodeSFX.Stop();
        correctCodeSFX.Stop();
        doorSFX.Stop();

    }

    
    public void b1()
    {
        charHolder.text += "1";
    }
    
    public void b2()
    {
        charHolder.text += "2";
    }
    
    public void b3()
    {
        charHolder.text += "3";
    }
    
    public void b4()
    {
        charHolder.text += "4";
    }
    
    public void b5()
    {
        charHolder.text += "5";
    }
    
    public void b6()
    {
        charHolder.text += "6";
    }
    
    public void b7()
    {
        charHolder.text += "7";
    }
    
    public void b8()
    {
        charHolder.text += "8";
    }
    
    public void b9()
    {
        charHolder.text += "9";
    }

    public void clearEvent()
    {
        charHolder.text = null;
        redLight.SetActive(false);
    }

    public void enterEvent()
    {
        if (charHolder.text == _keypadDoor.GetCode())
        {
            Debug.Log("Success!");
            correctCode = true;
            
            greenLight.SetActive(true);
            correctCodeSFX.Play();

            Invoke(nameof(OpenDoorSFX), 0.9f);
            
            Invoke(nameof(OpenDoor), 1);
        }
        else
        {
            Debug.Log("Failed");
            redLight.SetActive(true);
            incorrectCodeSFX.Play();
        }
    }


    private void OpenDoorSFX()
    {
        doorSFX.Play();
    }
    
    private void OpenDoor()
    {
        
        
        Destroy(_keypadDoor.gameObject);

        _wall.GetComponent<SpriteRenderer>().sortingOrder = 3;
        sceneChanger.SetActive(true);

        this.transform.parent.gameObject.SetActive(false);
    }
    
}
