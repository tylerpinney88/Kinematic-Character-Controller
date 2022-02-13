using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    [SerializeField] MouseLook mouseLook;
    [SerializeField] CharacterMovement characterMove;
    [SerializeField] JumpController characterJump;
    Vector2 mouseInput;


    PlayerInputs playerInput;
    PlayerInputs.MovementActions movement;

    bool mouseLocked;

    private void Awake()
    {
        mouseLocked = false;

        playerInput = new PlayerInputs();
        movement = playerInput.Movement;

        movement.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        movement.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();

        movement.Jump.started += onJump;
        movement.Jump.canceled += onJump;
    }

    private void Update()
    {
        mouseLook.RecieveInput(mouseInput, mouseLocked);
        characterMove.RecieveInput(movement.Move.ReadValue<Vector2>(), movement.Sprint.ReadValue<float>());
    }

    void onJump(InputAction.CallbackContext context)
    {
        characterJump.RecieveInput(context.ReadValueAsButton());
    }

    private void OnEnable()
    {
        movement.Enable();
    }

}
