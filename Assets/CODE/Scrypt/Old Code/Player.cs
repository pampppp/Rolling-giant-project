using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    public int nKey = 0;

    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    
    

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    private Vector3 slopeSlideVelocity;
    private float ySpeed;
    private bool isSliding;
    [HideInInspector]
    public bool canMove = true;

    [Header("sound")]
    public AudioClip[] stepsound;
    public AudioSource source;
    public bool walkSoundPlayed;
    public float timeSoundCd;

    public IEnumerator soundCd()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            timeSoundCd = .3f;
        }
        else
        {
            timeSoundCd = .7f;
        }
        yield return new WaitForSeconds(timeSoundCd);
        walkSoundPlayed = false;
    }
    public void PlayStepSound()
    {
        if (!walkSoundPlayed)
        {
            walkSoundPlayed = true;
            source.clip = stepsound[Random.RandomRange(0,stepsound.Length)];
            source.Play();
            StartCoroutine(soundCd());
        }
        
    }
    
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        if (Input.GetAxis("Vertical") != 0 && canMove && characterController.isGrounded || Input.GetAxis("Horizontal") != 0 && canMove && characterController.isGrounded)
        {
            PlayStepSound();
        }
        
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }
        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
    
}
