using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sneak : PlayerMovement
{
    public CharacterController PlayerHeight;
    public float normalHeight, sneakHeight;
    private float normalSpeed;
    [SerializeField] private float sneakSpeed;

    void Start()
    {
        normalSpeed = speed;
        sneakSpeed = normalSpeed / 2;
    }

    void Update()
    {
        // Sneak Mechanic
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            PlayerHeight.height = sneakHeight;
            speed = sneakSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            PlayerHeight.height = normalHeight;
            speed = normalSpeed;
        }
    }
}