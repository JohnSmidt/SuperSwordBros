using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private const float dodgeTime = 0.5f;
    private const float dodgeCooldownTime = 1.5f;
    private Rigidbody2D rb;

    float horizontal;
    float vertical;

    [SerializeField]
    private float moveSpeed, dodgeSpeed, stunTime;

    [SerializeField]
    private int healthPoints, blockPoints, manaPoints;

    // Enumerators
    //private enum direction;
    //private enum inventory;
    private bool isDodgeCooldown = false;
    private bool isPaused = false;
    private Vector2 dodgeDirection;
    private float currentDodgeTime; // Do I need this?
    private float currentDodgeCooldownTime;
    private bool canBlock = true;
    private bool isBlocking = false;
    private bool isDodging = false;
    private bool isMoving = false;
    private bool canDodge = true;
    private bool canMove = true;
    private bool isAttacking = false;
    private bool canAttack = true;
    private bool isStunned = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();        
    }

    // Checking controller presses
    void Update()
    {
        //Debug.Log(canDodge);
        if (Input.GetButtonDown("Dodge") && canDodge)
        {
            Debug.Log("Dodge Button Pressed");
            if (horizontal != 0 || vertical != 0)
            {
                isDodging = true;
                canDodge = false;
                dodgeDirection = new Vector2(horizontal, vertical);
                currentDodgeTime = dodgeTime;
                currentDodgeCooldownTime = dodgeCooldownTime;
            }
        }

        if (Input.GetButtonDown("Attack"))
        {
            Debug.Log("Attack Button Pressed");
        }
        if (Input.GetButtonDown("Block"))
        {
            Debug.Log("Block Button Pressed");
        }

        if(currentDodgeCooldownTime > 0)
        {
            currentDodgeCooldownTime -= Time.deltaTime;
        }
        else
        {
            canDodge = true;
        }
        
    }

    // Moving the player
    private void FixedUpdate()
    {
        // Base Movement
        if(!isPaused)
        {
            move();
        }

        
    }
    private void move()
    {
        if(canMove && !isDodging) // If the player can send movement commands...
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            Vector2 movement = new Vector2(horizontal, vertical);
            movement = movement.normalized * moveSpeed;
            rb.velocity = movement;
        }

        if(isDodging)
        {
            if(currentDodgeTime > 0)
            {
                dodgeDirection = dodgeDirection.normalized * dodgeSpeed;
                rb.velocity = dodgeDirection;
                currentDodgeTime -= Time.deltaTime;
            }
            else
            {
                isDodging = false;
                
            }
        }
        
    }
}
