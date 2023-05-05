using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hideable : MonoBehaviour
{
    [SerializeField] private EnemyController enemy;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.transform.CompareTag("Player"))
        {
            enemy.Hiding = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.transform.CompareTag("Player"))
        {
           enemy.Hiding = false;
        }
    }
}
