using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    CharacterController controller;
    CharacterMovement characterMove;

    [SerializeField] bool jumpActivated = false;
    [SerializeField] float jumpHeight = 3;

    bool isJumping;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        characterMove = GetComponent<CharacterMovement>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RecieveInput(bool jumpState)
    {
        Debug.Log(jumpState);

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
        if (jumpActivated)
        {
            characterMove.moveWithJump.y += jumpHeight;
        }

        else if (controller.isGrounded)
        {
            characterMove.moveWithJump.y = 0;
        }

        else if (characterMove.moveWithJump.y >= 0)
        {
            characterMove.moveWithJump.y -= Time.deltaTime * 1000;
        }
    }
}
