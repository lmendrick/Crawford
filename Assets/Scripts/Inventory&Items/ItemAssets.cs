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

    public Sprite item1Sprite;
    public Sprite item2Sprite;
    public Sprite item3Sprite;
    public Sprite item4Sprite;
    public Sprite item5Sprite;
    public Sprite keySprite;
}
