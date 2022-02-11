using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] MouseLook mouseLook;
    [SerializeField] CharacterMovement characterMove;
    Vector2 mouseInput;


    PlayerInputs playerInput;
    PlayerInputs.MovementActions movement;

    bool mouseLocked;

    float walkSpeed = 5f;
    float runSpeed = 10f;

    float currentSpeed;


    private void Awake()
    {
        mouseLocked = true;

        currentSpeed = walkSpeed;

        playerInput = new PlayerInputs();
        movement = playerInput.Movement;

        movement.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        movement.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();
    }

    private void Update()
    {
        mouseLook.RecieveInput(mouseInput, mouseLocked);
        characterMove.RecieveInput(movement.Move.ReadValue<Vector2>(), currentSpeed);
    }

    private void OnEnable()
    {
        movement.Enable();
    }

}
