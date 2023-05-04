using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float patrolDelay = 1;
    [SerializeField] private float patrolSpeed = 3;
    [SerializeField] private int damageAmount = 3;
    [SerializeField] private PlayerControllerTest _player;
    



    private Rigidbody2D _rb;
    private WaypointPath _waypointPath;
    private Vector2 _patrolTargetPosition;
    private Animator _animator;
    
    public  bool Hiding=false;


    // Awake is called before Start
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _waypointPath = GetComponentInChildren<WaypointPath>();
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        if (_waypointPath)
        {
            _patrolTargetPosition = _waypointPath.GetNextWaypointPosition();
        }
        else
        {
            //old patrolling code that was in Start() goes here
            while (true)
            {
                _rb.velocity = new Vector2(1, -1);
                yield return new WaitForSeconds(patrolDelay);
                _rb.velocity = new Vector2(-1, 1);
                yield return new WaitForSeconds(patrolDelay);
                StartCoroutine(nameof(Start));
            }
        }
    }

    private void FixedUpdate()
    {
        if (!_waypointPath) return;

        //set our direction toward that waypoint:
        //subtracting our position from target position
        //gives us the slope line between the two
        //We can get direction by normalizing it
        //We can get distance by magnitude
        var dir = _patrolTargetPosition - (Vector2)transform.position;

        //if we are close enough to the target,
        //time to get the next waypoint
        if (dir.magnitude <= 0.1)
        {
            //get next waypoint
            _patrolTargetPosition = _waypointPath.GetNextWaypointPosition();

            //change direction
            dir = _patrolTargetPosition - (Vector2)transform.position;
        }

        //this if/else is not in the video (it was made in the GameManager videos)
        //Be sure to update the line in the if clause to match the change in the
        //video instead of adding it above
        
            //UPDATE: how velocity is set
            //normalized reduces dir magnitude to 1, so we can
            //keep at the speed we want by multiplying
            _rb.velocity = dir.normalized * patrolSpeed; 
      
    }

    

    public void TakeHit()
    {
        _animator.Play("EnemyHit");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.transform.CompareTag("Player") && !_player.Crouching && !Hiding)
        {
            Debug.Log("Found");
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    
    
}

