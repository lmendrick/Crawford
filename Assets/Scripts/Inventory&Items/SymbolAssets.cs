using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolAssets : MonoBehaviour
{
    public static SymbolAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }



    public Sprite symbol1;
    public Sprite symbol2;
    public Sprite symbol3;
    public Sprite symbol4;
    public Sprite symbol5;
    public Sprite symbol6;
    public Sprite operand1;
    public Sprite operand2;
    public Sprite operand3;
    public Sprite operand4;
}
