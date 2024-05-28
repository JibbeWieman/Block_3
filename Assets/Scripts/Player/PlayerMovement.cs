using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    // Reference to the player's camera
    public GameObject playerCamera;
    public GameObject rcCamera;

    // Movement speeds
    public float walkSpeed = 12f;
    public float normalSpeed;
    public float crouchSpeed;

    // Jumping parameters
    public float jumpPower = 7f;
    private bool isJumping = false;
    public float gravity = -9.81f;

    // Look sensitivity
    static public float lookSpeedX = 2f;
    static public float lookSpeedY = 2f;
    public float lookXLimit = 75f;

    // Height parameters
    public float normalHeight, crouchHeight;
    
    // Private variables for movement
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    // Static variables
    static bool isCrouching = false;
    static public bool canMove = true;

    //RC variables
    public GameObject rcBatteryUI;

    void Start()
    {
        // Get reference to CharacterController component
        characterController = GetComponent<CharacterController>();

        // Set variables
        crouchSpeed = walkSpeed / 2;
        normalSpeed = walkSpeed;
        normalHeight = characterController.height;
        crouchHeight = normalHeight/2;
    }

    void Update()
    {
        #region RC Code
        if (Input.GetKeyDown(KeyCode.R))
        {
            RCmovement.rcActivated = !RCmovement.rcActivated;
        }

        if (RCmovement.rcActivated)
        {
            rcBatteryUI.SetActive(true);
            rcCamera.SetActive(true);
            playerCamera.SetActive(false);

        } else
        {
            rcBatteryUI.SetActive(false);
            rcCamera.SetActive(false);
            playerCamera.SetActive(true);
        }

        #endregion

        #region canMove Code
        if (PlayerHealth.isDead || PauseMenu.GameIsPaused || RCmovement.rcActivated)
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }
        #endregion

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

        #region Handle jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded && !isCrouching)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }
        #endregion

        // Apply gravity
        if (!characterController.isGrounded)
        {
            moveDirection.y += gravity * Time.deltaTime;
            isJumping = true;
        }
        else 
        {
            isJumping = false;
        } 

        #region Handle crouching
        if (Input.GetKeyDown(KeyCode.LeftShift) && canMove && !isJumping)
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
        #endregion

        // Move the player
        characterController.Move(moveDirection * Time.deltaTime);

        #region Handle player rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeedX;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedY, 0);
        }
        #endregion
    }
}