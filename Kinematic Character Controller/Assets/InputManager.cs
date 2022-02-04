using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] MouseLook mouseLook;
    [SerializeField] CharacterController characterController;

    Vector2 mouseInput;


    PlayerInputs playerInput;
    PlayerInputs.MovementActions movement;


    private void Awake()
    {
        playerInput = new PlayerInputs();
        movement = playerInput.Movement;

        movement.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        movement.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();

        movement.Jump.started += ctx => RegisterJump();
    }

    private void Update()
    {
        mouseLook.RecieveInput(mouseInput);
    }

    private void OnEnable()
    {
        movement.Enable();

    }

    private void RegisterJump()
    {
        characterController.RecieveJump();
    }


}
