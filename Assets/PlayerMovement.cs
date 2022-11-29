using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private const float dodgeTime = 0.33f;
    private const float dodgeCooldownTime = dodgeTime + 0.5f;
    private Rigidbody2D rb;

    float horizontal;
    float vertical;

    [SerializeField]
    private float moveSpeed, dodgeSpeed, stunTime;

    private GameObject primaryWeapon;
    private GameObject secondaryWeapon;

    [SerializeField]
    private int healthPoints, blockPoints, manaPoints;

    // Enumerators
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
        horizontal = 0;
        vertical = 0;
        rb = GetComponent<Rigidbody2D>();        
    }

    // Checking controller presses
    void Update()
    {
        // Basic Movement
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 || vertical != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }



        //Debug.Log(canDodge);
        if (Input.GetButtonDown("Dodge"))
        {
            // Can we dodge?
            if(canDodge)
            {
                Debug.Log("Dodging...");
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
       
        if(!isPaused)
        {
            // Base Movement
            if (canMove && !isDodging) // If the player can send movement commands...
            {
                Vector2 movement = new Vector2(horizontal, vertical);
                movement = movement.normalized * moveSpeed;
                rb.velocity = movement;
            }

            // Dodging
            if (isDodging)
            {
                if (currentDodgeTime > 0)
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
}
