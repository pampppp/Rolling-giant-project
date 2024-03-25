using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Header("External Components")]
    [SerializeField] private Rigidbody playerRigidBody;
    [SerializeField] private Transform playerOrientation;
    [SerializeField] private CapsuleCollider playerCollider;
    [SerializeField] private LayerMask groundLayer;

    [Header("Movement Variables")]
    [SerializeField] private float walkingSpeed = 5f;
    [SerializeField] private float runningSpeed = 7f;
    [SerializeField] private float playerDrag = 1f;
    private float currentSpeed;
    private Vector3 moveDirection;

    [Header("Stamina Variables")]
    [SerializeField] private float playerStamina = 100f;
    [SerializeField] private float playerStaminaMax = 100f;
    [SerializeField] private float staminaDrainRate = 1f;
    [SerializeField] private float staminaRechargeTime = 1f;
    [SerializeField] private float playerFatigueTimer = 0f;
    [SerializeField] private float exponentialPenalty = 1f;
    private bool isRunning;
    private bool isFatigued;

    [Header("Jump Variables")]
    [SerializeField] private float jumpHeight = 4f;
    [SerializeField] private float jumpCooldown = 20f;
    [SerializeField] private float airMultiplier = 1f;
    private bool isJumping;
    private bool readyToJump;

    //Ground Check Variables
    private float radius;
    private Vector3 groundCheckPosition;
    private bool isGrounded;

    //Input Variables
    private float horizontalInput;
    private float verticalInput;
    private KeyCode sprintKey = KeyCode.LeftShift;
    private KeyCode jumpKey = KeyCode.Space;

    void Start()
    {
        readyToJump = true;
    }

    void Update()
    {
        GroundCheck();

        GetKeyboardInputs();
        SpeedControl();

        Console.WriteLine("Stamina: " + playerStamina);

        if (isGrounded) playerRigidBody.drag = playerDrag;
        else playerRigidBody.drag = 0f;
    }

    private void FixedUpdate()
    {
        MovePlayer();
        ReduceStamina();

        if (isJumping && readyToJump && isGrounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void GetKeyboardInputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        isJumping = Input.GetKey(jumpKey); 
    }

    private void MovePlayer()
    {
        moveDirection = playerOrientation.forward * verticalInput + playerOrientation.right * horizontalInput;

        if (isGrounded) playerRigidBody.AddForce(moveDirection.normalized * currentSpeed * 10f, ForceMode.Force);
        else if(!isGrounded) playerRigidBody.AddForce(moveDirection.normalized * currentSpeed * 10f * airMultiplier, ForceMode.Force);

    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(playerRigidBody.velocity.x, 0f, playerRigidBody.velocity.z);

        if(!Input.GetKey(sprintKey) && flatVelocity.magnitude > walkingSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * walkingSpeed;
            playerRigidBody.velocity = new Vector3(limitedVelocity.x, playerRigidBody.velocity.y, limitedVelocity.z);
        }
        else if (Input.GetKey(sprintKey) && flatVelocity.magnitude > runningSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * runningSpeed;
            playerRigidBody.velocity = new Vector3(limitedVelocity.x, playerRigidBody.velocity.y, limitedVelocity.z);
        }

        Console.WriteLine("Speed: " + flatVelocity.magnitude);
    }

    private void GroundCheck()
    {
        radius = playerCollider.radius * 0.9f;
        groundCheckPosition = transform.position + Vector3.up * (radius * 0.9f);

        isGrounded = Physics.CheckSphere(groundCheckPosition, radius, groundLayer);
    }

    private void Jump()
    {
        playerRigidBody.velocity = new Vector3(playerRigidBody.velocity.x, 0f, playerRigidBody.velocity.z);

        playerRigidBody.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    public void ReduceStamina()
    {
        currentSpeed = walkingSpeed;

        if (Input.GetKey(sprintKey))
        {
            if (playerStamina > 0 && !isFatigued)
            {
                currentSpeed = runningSpeed;
                isRunning = true;
            }
            else if (isRunning || isFatigued)
            {
                currentSpeed = walkingSpeed;
                isRunning = false;

                exponentialPenalty = 1;
            }

            exponentialPenalty += Time.deltaTime / 20f;
        }

        if (Input.GetKeyUp(sprintKey) && isRunning || isFatigued)
        {
            currentSpeed = walkingSpeed;
            isRunning = false;
        }

        if (!Input.GetKey(sprintKey) && exponentialPenalty > 1)
        {
            exponentialPenalty -= Time.deltaTime / 20f;

            if (exponentialPenalty < 1) exponentialPenalty = 1f;
        }

        if (isRunning)
        {
            playerStamina -= (Time.deltaTime * staminaDrainRate * exponentialPenalty);
            playerStamina += Time.deltaTime * staminaRechargeTime;
        }
        else if (!isFatigued)
        {
            playerStamina += Time.deltaTime * staminaRechargeTime;
        }

        if (playerStamina <= 0f && playerFatigueTimer <= 3)
        {
            playerFatigueTimer += Time.deltaTime;
            isFatigued = true;
        }
        else if (playerFatigueTimer >= 3)
        {
            playerStamina += Time.deltaTime * staminaRechargeTime;
            isFatigued = false;
            playerFatigueTimer = 0;
        }

        if (playerStamina < 0f) playerStamina = 0f;

        if (playerStamina > playerStaminaMax) playerStamina = playerStaminaMax;
    }
}
