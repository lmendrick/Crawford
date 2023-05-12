using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPopUpAssets : MonoBehaviour
{
    public static ItemPopUpAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Transform pfItemWorld;

    public Sprite Crowbar;
    public Sprite Key;
    public Sprite puzzleP1;
    public Sprite puzzleP2;
    public Sprite puzzleP3;
    public Sprite puzzleP4;
    public Sprite Book;
    public Sprite OpenBook;
    public Sprite OpenBookWithKey;
}
