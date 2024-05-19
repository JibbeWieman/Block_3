using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController Controller;

    [SerializeField] private float speed = 12f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;
    public bool isSneaking = false;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    // Sneak Variables
    private float normalHeight, sneakHeight;
    private float normalSpeed;
    private float sneakSpeed;

    private void Start()
    {
        Controller = GetComponent<CharacterController>();
        normalSpeed = speed;
        sneakSpeed = normalSpeed / 2;
        normalHeight = Controller.height;
        sneakHeight = normalHeight / 2;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Sneak Mechanic
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Controller.height = sneakHeight;
            speed = sneakSpeed;
            isSneaking = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Controller.height = normalHeight;
            speed = normalSpeed;
            isSneaking = false;
        }

        Vector3 move = transform.right * x + transform.forward * z;

        Controller.Move(speed * Time.deltaTime * move);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        Controller.Move(velocity * Time.deltaTime);
    }
}
