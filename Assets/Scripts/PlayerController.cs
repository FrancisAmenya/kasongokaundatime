using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class handles player jumping and crouching
public class PlayerController : MonoBehaviour
{
   [SerializeField] private SunStateManager sunStateManager;

    private bool touchingGround, dead; 
    public bool jumping, crouching, standingUp;
    private float groundLength = 0.8f;

    [Header("References ---")]
    public GameController2 gameController2;
    public Animator animator;
    public AudioSource audio;
    public AudioClip jumpSound, deathSound;

    public LayerMask groundLayer;
    public Rigidbody2D rigidBody;
    public EdgeCollider2D standingCollider;
    public BoxCollider2D crouchCollider;

    [Header("Variables ---")]
    public float jumpForce; //Ideal: 7,1,3
    public float gravity;
    public float fallMultiplier;

    void Start()
    {   
         // Find the SunStateManager in the scene
        sunStateManager = GameObject.FindObjectOfType<SunStateManager>();
        Debug.Log("SunStateManager found: " + (sunStateManager != null));  // Debugging line
        
        jumping = false;
        crouching = false;
        standingUp = false;
    }
    //Update is called once per frame
    void Update()
    {
        if(dead == false) //While alive, player can perform actions
        {
            modifyPhysics();
            //Check with a small raycast at dinosaur's feet if we're touching the ground
            touchingGround = Physics2D.Raycast(transform.position, Vector2.down, groundLength, groundLayer);

            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || jumping) && touchingGround) //If we press space and we're touching ground, jump
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || crouching) //If we press down, dinosaur crouches
            {
                sunStateManager.SunIsRaging();
                Crouch();
            }
            else if (!(Input.GetKey(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && !crouching) //If we're not pressing down, stand up
            {
                sunStateManager.SunIsSnoring();
                StandUp();
            }
        }
    }

    public void Jump()
    {
        audio.PlayOneShot(jumpSound);
        sunStateManager.SunIsTearing();
        JumpAction();
        jumping = false;
    }

    public void EnableJumpBool()
    {
        jumping = true;
    }

    public void EnableCrouchBool()
    {
        crouching = true;
    }

    public void DisableCrouchBool()
    {
        crouching = false;
    }

    public void Crouch()
    {
        animator.Play("Crouch");
        standingCollider.enabled = false;
        crouchCollider.enabled = true;
        //crouching = false;
    }

    public void StandUp()
    {
        animator.Play("Walk");
        standingCollider.enabled = true;
        crouchCollider.enabled = false;
    }

    //Function executed when player wants to jump by pressing space
    private void JumpAction()
    {
        //Impulse player body upward
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        animator.Play("Walk");
        standingCollider.enabled = true;
        crouchCollider.enabled = false;
    }

    //Function that handles jump feel
    private void modifyPhysics()
    {
        if (touchingGround)
        {
            rigidBody.gravityScale = 0;
        }
        else
        {
            rigidBody.gravityScale = gravity;
            if(rigidBody.velocity.y < 0)
            {
                rigidBody.gravityScale = gravity * fallMultiplier;
            }
            else if(rigidBody.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
            {
                rigidBody.gravityScale = gravity * (fallMultiplier / 2);
            }
        }
    }

    //Function executed when player collides with something
    private void OnCollisionEnter2D(Collision2D other)
    {
        //If player collides with something, game stops and Game Over triggers
        if (other.gameObject.CompareTag("Enemy"))
        {
            dead = true;
            audio.PlayOneShot(deathSound);
            animator.Play("Death");

            sunStateManager.SunIsHappy();

            //Tell the Game Controller script that we lost
            gameController2.ActivateGameOver();
        }
    }
}
