using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

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
}
