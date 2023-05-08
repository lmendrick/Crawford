using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class Symbols
{

    public enum SymbolType
    {
        one,
        two,
        three,
        four,
        five,
        six,
        op1,
        op2,
        op3,
        op4
    }

    public SymbolType symbolType;

    public Sprite GetSprite()
    {
        switch (symbolType)
        {
            default:
            case SymbolType.one: return SymbolAssets.Instance.symbol1;
            case SymbolType.two: return SymbolAssets.Instance.symbol2;
            case SymbolType.three: return SymbolAssets.Instance.symbol3;
            case SymbolType.four: return SymbolAssets.Instance.symbol4;
            case SymbolType.five: return SymbolAssets.Instance.symbol5;
            case SymbolType.six: return SymbolAssets.Instance.symbol6;
            case SymbolType.op1: return SymbolAssets.Instance.operand1;
            case SymbolType.op2: return SymbolAssets.Instance.operand2;
            case SymbolType.op3: return SymbolAssets.Instance.operand3;
            case SymbolType.op4: return SymbolAssets.Instance.operand4;

        }


    }
}

