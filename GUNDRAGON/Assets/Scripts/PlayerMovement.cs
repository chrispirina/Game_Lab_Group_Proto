using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;

    float horizontal;
    float vertical;
    public float moveSpeed = 6.0f;
    public float startSpeed = 0.0f;
    public bool isMoving = false;

    public float dodgeStrength = 5.0f;
    public float maxDodgeTime = 1.0f;
    public float dodgeTimer = 0.0f;
    public bool didDodge = false;


    
    public float gravityMotion = 0.0f; //Defines y axis movement of controller
    public float gravityMultiplier; // Defines modifieer on GravityMotion to increase/decrease fall speed.
    public float jumpStrength = 10.0f; // How high controller can jump.
    public float maxJumpTime = 1.0f; // The maximum limit of the jumpTimer.
    public float jumpTimer = 0.0f; // Counts how long until the controller reaches the apex of its jump. 
    public bool didJump = false; // Is the controller jumping now.
    

	// Use this for initialization
	void Start ()
    {
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        moveDirection = new Vector3(horizontal, 0, vertical);
        moveDirection = moveDirection * moveSpeed;

        WalkStart();

        DodgeCooldown();
       

        if (controller.isGrounded == true)
        {
            didJump = false;
            gravityMotion = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gravityMotion = jumpStrength;
                jumpTimer = maxJumpTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (startSpeed > 0 && didDodge == false)
            {
                dodgeTimer = maxDodgeTime;
            }   
            else
            {
                Debug.Log("Could not dodge");
            }         
        }

        isJumping();
        

        moveDirection.y = gravityMotion;

        moveDirection = moveDirection * Time.deltaTime;

        if (startSpeed < 0.4f || didJump == true)
        {
            moveDirection.x = moveDirection.x / 2;
            moveDirection.z = moveDirection.z / 2;
        }

        if (didDodge == true)
        {
            moveDirection.x = moveDirection.x * dodgeStrength;
            moveDirection.z = moveDirection.z * dodgeStrength;
        }

        controller.Move(moveDirection); // Character moves following transform information.
	}

    public void WalkStart()
    {
        if (moveDirection.x  != 0 || moveDirection.z != 0)
        {
            startSpeed += Time.deltaTime;
            isMoving = true;
        }
        
       else if (moveDirection.z == 0 && moveDirection.x == 0)
        {
            startSpeed = 0;
            isMoving = false;
        }
    }

    public void DodgeCooldown()
    {
        if (dodgeTimer > 0)
        {
            dodgeTimer -= Time.deltaTime;
            didDodge = true;
        }
        else if (dodgeTimer <= 0)
        {
            didDodge = false;
        }
    }

    public void isJumping()
    {
        if (jumpTimer <= 0)
        {
            gravityMotion -= 9.8f * gravityMultiplier * Time.deltaTime;

        }

        else if (jumpTimer > 0)
        {
            gravityMotion += 9.8f * jumpStrength * Time.deltaTime * 0.5f;
            jumpTimer -= Time.deltaTime * 2;
            didJump = true;
        }

    }

}
