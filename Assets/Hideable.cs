using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hideable : MonoBehaviour
{
    [SerializeField] private EnemyController enemy;
    
    public void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Hiding");
        if (other.transform.CompareTag("Player"))
        {
            enemy.Hiding = true;
        }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
           enemy.Hiding = false;
        }
    }
}
