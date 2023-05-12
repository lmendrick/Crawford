using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[Serializable]
public class Item {

    public enum ItemType
    {
        Crowbar,
        Key,
        puzzleP1,
        puzzleP2,
        puzzleP3,
        puzzleP4,
        Book,
        BookWithKey
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Crowbar: return ItemAssets.Instance.Crowbar;
            case ItemType.Key: return ItemAssets.Instance.Key;
            case ItemType.puzzleP1: return ItemAssets.Instance.puzzleP1;
            case ItemType.puzzleP2: return ItemAssets.Instance.puzzleP2;
            case ItemType.puzzleP3: return ItemAssets.Instance.puzzleP3;
            case ItemType.puzzleP4: return ItemAssets.Instance.puzzleP4;
            case ItemType.Book: return ItemAssets.Instance.Book;
            case ItemType.BookWithKey: return ItemAssets.Instance.Book;
           
        }

    }
    public Sprite GetPopUpSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Crowbar: return ItemPopUpAssets.Instance.Crowbar;
            case ItemType.Key: return ItemPopUpAssets.Instance.Key;
            case ItemType.puzzleP1: return ItemPopUpAssets.Instance.puzzleP1;
            case ItemType.puzzleP2: return ItemPopUpAssets.Instance.puzzleP2;
            case ItemType.puzzleP3: return ItemPopUpAssets.Instance.puzzleP3;
            case ItemType.puzzleP4: return ItemPopUpAssets.Instance.puzzleP4;
            case ItemType.BookWithKey: return ItemPopUpAssets.Instance.Book;
           
        }

    }



  

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Key:
          
                    return false;
                case ItemType.puzzleP1:
                    return false;
                case ItemType.puzzleP2:
                    return false;
                case ItemType.puzzleP3:
                    return false;
                case ItemType.puzzleP4:
                    return false;
        }
    }
 }

