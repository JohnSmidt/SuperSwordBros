using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private int dodgeStaminaDrain = 30;
   
    // Direction
    float horizontal;
    float vertical;

    [SerializeField]
    private float moveSpeed, dodgeSpeed, stunTime, 
                  staminaPoints, staminaRegenSpeed,
                  healthPoints, manaPoints, manaRegenSpeed,
                  dodgeTime = 0.33f, staminaCapacity = 100f;

    private GameObject primaryWeapon;
    private GameObject secondaryWeapon;

    private bool isPaused = false;
    private Vector2 dodgeDirection;
    private float currentDodgeTime;
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
            canDodge = true;
        }
        else
        {
            isMoving = false;
            canDodge = false;
        }

        if (Input.GetButtonDown("Dodge") && staminaPoints > 0)
        {
            // Can we dodge?
            if (canDodge)
            {
                Debug.Log("Dodging...");
                staminaPoints -= dodgeStaminaDrain;
                isDodging = true;
                canDodge = false;
                dodgeDirection = new Vector2(horizontal, vertical);
                currentDodgeTime = dodgeTime;
            }
        }

        if (Input.GetButtonDown("Attack"))
        {
            Debug.LogWarning("Attack Button Not Implemented");
        }
        if (Input.GetButtonDown("Block"))
        {
            Debug.LogWarning("Block Button Not Implemented");
        }
    }

    // Moving the player
    private void FixedUpdate()
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
        if (staminaPoints >= staminaCapacity)
        {
            staminaPoints = staminaCapacity;
        }
        else
        {
            staminaPoints += Time.deltaTime * staminaRegenSpeed;
        }
    }
}
