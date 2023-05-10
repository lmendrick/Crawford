using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;


public class Password : MonoBehaviour
{
    public Sprite symbol1;
    public Sprite symbol2;
    public Sprite symbol3;
    public Sprite symbol4;
    public Sprite symbol5;
    public Sprite symbol6;
    public Sprite symbol7;
    public Sprite op1;
    public Sprite op2;
    public Sprite op3;
    public Sprite op4;
    
    // Start is called before the first frame update

    void Start()
    {
        

    }

    public void display()
    {
     
      transform.Find("Eqn1/Symbol1").GetComponent<Image>().sprite=symbol1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
        
    }
}
