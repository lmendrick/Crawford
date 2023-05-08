using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

[Serializable]

public class Password : MonoBehaviour
{
    public Sprite symbol1;
    public Sprite symbol2;
    public Sprite symbol3;
    public Sprite symbol4;
    public Sprite symbol5;
    public Sprite symbol6;
    public Sprite op1;
    public Sprite op2;
    public Sprite op3;
    public Sprite op4;
    
    // Start is called before the first frame update
    private List<Sprite> list;

    void Start()
    {

    }

    public void display()
    {
        list.Add(SymbolAssets.Instance.symbol1);
        list.Add(SymbolAssets.Instance.symbol2);
        list.Add(SymbolAssets.Instance.symbol3);
        list.Add(SymbolAssets.Instance.symbol4);
        list.Add(SymbolAssets.Instance.symbol5);
        list.Add(SymbolAssets.Instance.symbol6);
        Random rnd = new Random();
        list.RemoveAt(rnd.Next(0, list.Count - 1));
        list.RemoveAt(rnd.Next(0, list.Count - 1));
        Sprite sym1 = list[0];
        Sprite sym2 = list[1];
        Sprite sym3 = list[2];
        Sprite sym4 = list[3];
        transform.Find("Eq1/Symbo1").GetComponent<Image>().sprite = sym1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
