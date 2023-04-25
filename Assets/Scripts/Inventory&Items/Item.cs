using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[Serializable]
public class Item {

    public enum ItemType
    {
        Item1,
        Item2,
        Key,
        puzzleP1,
        puzzleP2,
        puzzleP3
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Item1: return ItemAssets.Instance.item1Sprite;
            case ItemType.Item2: return ItemAssets.Instance.item2Sprite;
            case ItemType.Key: return ItemAssets.Instance.Key;
            case ItemType.puzzleP1: return ItemAssets.Instance.puzzleP1;
            case ItemType.puzzleP2: return ItemAssets.Instance.puzzleP2;
            case ItemType.puzzleP3: return ItemAssets.Instance.puzzleP3;
           
        }

    }
    public Sprite GetPopUpSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Item1: return ItemPopUpAssets.Instance.item1Sprite;
            case ItemType.Item2: return ItemPopUpAssets.Instance.item2Sprite;
            case ItemType.Key: return ItemPopUpAssets.Instance.Key;
            case ItemType.puzzleP1: return ItemPopUpAssets.Instance.puzzleP1;
            case ItemType.puzzleP2: return ItemPopUpAssets.Instance.puzzleP2;
            case ItemType.puzzleP3: return ItemPopUpAssets.Instance.puzzleP3;
           
        }

    }


  

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
                case ItemType.Item1:
                return true;
                case ItemType.Item2:
                case ItemType.Key:
          
                    return false;
                case ItemType.puzzleP1:
                    return false;
                case ItemType.puzzleP2:
                    return false;
                case ItemType.puzzleP3:
                    return false;
        }
    }
 }

