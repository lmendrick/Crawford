
using System;
using UnityEngine;
using UnityEngine.InputSystem; //Don't miss this!

public class PlayerControllerTest : MonoBehaviour
{

    public bool Crouching;

    private PlayerInput input; //field to reference Player Input component
    private Rigidbody2D _rigidbody;

    public float speed = 5f;
    public Animator animator;
    private Vector2 movement;

    private bool _FacingRight = true;
    

    private bool isOnStairs;
    

    // Start is called before the first frame update
    void Start()
    {
        //set reference to PlayerInput component on this object
        //Top Action Map, "Player" should be active by default
        //_input = GetComponent<PlayerInput>();
        //You can switch Action Maps using _input.SwitchCurrentActionMap("UI");

        //set reference to Rigidbody2D component on this object
        //_rigidbody = GetComponent<Rigidbody2D>();

        //transform.position = new Vector2(3, -1);
        //Invoke(nameof(AcceptDefeat), 10);
    }

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isOnStairs = false;

    }

    // Update is called once per frame
    void Update()
    {

        //movement.y = Input.GetAxisRaw("Vertical");
        movement.x = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("xvelocity", movement.sqrMagnitude);


        if (input.actions["Crouch"].WasPressedThisFrame())
        {
            if(!Crouching)
            {
                Crouching = true;
                animator.SetBool("isCrouched", true);
                speed = 3f;
            }
            else
            {
                animator.SetBool("isCrouched", false);
                Crouching = false;
                speed = 5f;
            }
        }
        
    }

    private void FixedUpdate()
    {

        _rigidbody.MovePosition(_rigidbody.position + movement * speed * Time.fixedDeltaTime);

       if (movement.x > 0 && !_FacingRight)
       {
         Flip();
       }

       else if (movement.x < 0 && _FacingRight)
       {
           Flip();
       }

    }


	private void Flip()
	{
        
        
		// Switch the way the player is labelled as facing.
		_FacingRight = !_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
        
        
	}

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.transform.CompareTag("Stairs") && Crouching)
        {
            isOnStairs = true;
            speed = 5f;
        }
        else if (!other.transform.CompareTag("Stairs") && Crouching)
        {
            speed = 3f;
        }
        else
        {
            speed = 5f;
        }
        
    }

    // private void OnCollisionExit(Collision other)
    // {
    //     if (other.transform.CompareTag("Stairs"))
    //     {
    //         isOnStairs = false;
    //     }
    // }
}


