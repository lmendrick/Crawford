using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;


public class Password : MonoBehaviour
{
    private List<int> usedSymbols;
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
    private int sym1;
    private int sym2;
    private int sym3;
    private int sym4;
    
    
    // Start is called before the first frame update

    void Start()
    {
        usedSymbols = new List<int>();
        for (int i = 1; i < 8; i++)
        {
            usedSymbols.Add(i);
        }

        Random rand = new Random();
        usedSymbols.RemoveAt(rand.Next(3,6));
        usedSymbols.RemoveAt(rand.Next(3,5));
        usedSymbols.RemoveAt(rand.Next(0,2));
        for (int i = 0; i < usedSymbols.Count; i++)
        {
            Debug.Log(usedSymbols[i]);
        }


    }

    public void display()
    {
        sym1 = usedSymbols[3];
        sym2 = usedSymbols[2];
        sym3 = usedSymbols[1];
        sym4 = usedSymbols[0];
        transform.Find("Eqn1/Symbol1").GetComponent<Image>().sprite=setSymbol(sym2);
        transform.Find("Eqn1/Symbol2").GetComponent<Image>().sprite = setSymbol(sym4);
        transform.Find("Eqn2/Symbol1").GetComponent<Image>().sprite=setSymbol(sym1);
        transform.Find("Eqn2/Symbol2").GetComponent<Image>().sprite = setSymbol(sym3);
        transform.Find("Eqn3/Symbol1").GetComponent<Image>().sprite=setSymbol(sym2);
        transform.Find("Eqn3/Symbol2").GetComponent<Image>().sprite = setSymbol(sym1);
        if (sym1 == 6)
        {
            transform.Find("Eqn4/Symbol1").GetComponent<Image>().sprite = setSymbol(sym1);
        }
        else if (sym2 == 6)
        {
            transform.Find("Eqn4/Symbol1").GetComponent<Image>().sprite = setSymbol(sym2);
        }
        
        transform.Find("Eqn4/Symbol2").GetComponent<Image>().sprite=setSymbol(sym3);
        
    }

    public Sprite setSymbol(int num)
    {
        switch (num)
        {
            default:
            case 1:
                return symbol1;
            case 2:
                return symbol2;
            case 3:
                return symbol3;
            case 4:
                return symbol4;
            case 5:
                return symbol5;
            case 6:
                return symbol6;
            case 7:
                return symbol7;
        }
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
