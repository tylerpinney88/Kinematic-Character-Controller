using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    CharacterController controller;
    CharacterMovement characterMove;

    bool jumpActivated = false;
    [SerializeField] float jumpTime = 2;
    float time = 0;

    [HideInInspector] public bool isJumping;

    [SerializeField] AnimationCurve jumpCurve;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        characterMove = GetComponent<CharacterMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        if (jumpActivated)
        {
            isJumping = true;
        }

        if (isJumping)
        {
            time += Time.deltaTime;
        }

        if (time >= jumpTime)
        {
            isJumping = false;
            jumpActivated = false;
            time = 0;
        }
    }

    public void RecieveInput(bool jumpState)
    {
        if (controller.isGrounded && jumpState == true)
        {
            jumpActivated = true;
        }

        else
        {
            jumpActivated = false;
        }
    }

    public void ApplyJump()
    {
        if (isJumping)
        {
            characterMove.moveWithJump.y = (jumpCurve.Evaluate(time) * 6);
            if (time >= 0.5 && controller.isGrounded)
            {
                isJumping = false;
                jumpActivated = false;
                time = 0;
            }
        }

        else
        {
            characterMove.moveWithJump.y = 0;
        }
    }
}
