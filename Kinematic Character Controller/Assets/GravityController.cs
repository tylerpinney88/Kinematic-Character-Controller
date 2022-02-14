using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    CharacterController controller;
    CharacterMovement movement;

    [SerializeField] float Gravity;
    [SerializeField] float groundedGrav = -2.5f;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        movement = GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {   
    }

    public void ApplyGravity()
    {
        if (!controller.isGrounded)
        {
            movement.moveWithGrav.y += Gravity * Time.deltaTime;
        }

        else if (controller.isGrounded)
        {
            movement.moveWithGrav.y = groundedGrav;
        }
    }
}
