using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Hideable : MonoBehaviour
{
    [FormerlySerializedAs("enemy")] [SerializeField] private EnemyController enemy1;
    [SerializeField] private EnemyController enemy2;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.transform.CompareTag("Player"))
        {
            enemy1.Hiding = true;
            enemy2.Hiding = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.transform.CompareTag("Player"))
        {
           enemy1.Hiding = false;
           enemy2.Hiding = false;
        }
    }
}
