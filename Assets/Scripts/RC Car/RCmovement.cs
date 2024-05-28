using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RCmovement : MonoBehaviour
{
    // Reference to the player's camera
    public Camera rcCamera;

    // Movement speeds
    public float walkSpeed = 8f;

    // Jumping parameters
    public float gravity = -9.81f;

    // Look sensitivity
    private float lookSpeedX = PlayerMovement.lookSpeedX / 2;
    private float lookSpeedY = PlayerMovement.lookSpeedY / 2;
    private float lookXLimit = .3f;

    // Private variables for movement
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;
    static public bool rcActivated = false;

    void Start()
    {
        // Get reference to CharacterController component
        characterController = GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (rcActivated)
        {
            if (other.gameObject.CompareTag("Landlord"))
            {
                Debug.Log("Collision detected with " + other.gameObject.name); // Log the collision object
                //GuardBT.Stunned(3f);
            }
        }
    }

    void Update()
    {
        #region rcActivated Code
        if (PlayerHealth.isDead || PauseMenu.GameIsPaused || PlayerMovement.canMove)
        {
            rcActivated = false;
        }
        else
        {
            rcActivated = true;
        }
        #endregion

        // Get the direction of movement based on player's input
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        //Vector3 right = transform.TransformDirection(Vector3.right);

        // Calculate movement speed based on input
        float curSpeedX = rcActivated ? walkSpeed * Input.GetAxis("Vertical") : 0;
        //float curSpeedY = rcActivated ? walkSpeed * Input.GetAxis("Horizontal") : 0;

        // Store the current vertical movement direction
        float movementDirectionY = moveDirection.y;

        // Combine the movement directions
        moveDirection = (forward * curSpeedX);

        // Apply gravity
        if (!characterController.isGrounded)
        {
            moveDirection.y += gravity * Time.deltaTime;
        }

        // Move the player
        characterController.Move(moveDirection * Time.deltaTime);

        #region Handle player rotation
        if (rcActivated)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeedX;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            rcCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedY, 0);
        }
        #endregion
    }
}