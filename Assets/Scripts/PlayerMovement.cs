using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Reference to the player's camera
    public Camera playerCamera;

    // Movement speeds
    public float walkSpeed = 12f;
    public float normalSpeed;
    public float crouchSpeed;

    // Jumping parameters
    public float jumpPower = 7f;
    public float gravity = -9.81f;

    // Look sensitivity
    public float lookSpeedX = 2f;
    public float lookSpeedY = 2f;
    public float lookXLimit = 45f;

    // Height parameters
    public float normalHeight, crouchHeight;

    // Private variables for movement
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;
    private bool canMove = true;

    // Static movement variables
    static bool isCrouching = false;

    void Start()
    {
        // Get reference to CharacterController component
        characterController = GetComponent<CharacterController>();

        // Lock cursor and show it
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true; // Set to true to always show the cursor

        // Set variables
        crouchSpeed = walkSpeed / 2;
        normalSpeed = walkSpeed;
        normalHeight = characterController.height;
        crouchHeight = normalHeight / 2;
    }

    void Update()
    {
        // Get the direction of movement based on player's input
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Calculate movement speed based on input
        float curSpeedX = canMove ? walkSpeed * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? walkSpeed * Input.GetAxis("Horizontal") : 0;

        // Store the current vertical movement direction
        float movementDirectionY = moveDirection.y;

        // Combine the movement directions
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        // Handle jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity
        if (!characterController.isGrounded)
        {
            moveDirection.y += gravity * Time.deltaTime;
        }

        // Handle crouching
        if (Input.GetKeyDown(KeyCode.LeftShift) && canMove)
        {
            characterController.height = crouchHeight;
            walkSpeed = crouchSpeed;
            isCrouching = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            characterController.height = normalHeight;
            walkSpeed = normalSpeed;
            isCrouching = false;
        }

        // Move the player
        characterController.Move(moveDirection * Time.deltaTime);

        // Handle player rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeedX;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedY, 0);
        }
    }
}
