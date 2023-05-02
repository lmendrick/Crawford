using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverSound : MonoBehaviour
{
    public AudioSource mySounds;
    public AudioClip hoverSound;

    public void Hover()
    {
    mySounds.PlayOneShot(hoverSound);
    }
}
