using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInput:MonoBehaviour
{
    // New Input System
    private PlayerControls playerControls;
    private InputAction moveAction;
    private InputAction sprintAction;
    private InputAction crouchAction;
    public Vector2 moveInput;
    public bool isSprinting;
    public bool isCrouching;
    void Awake()
    {
        playerControls=new PlayerControls();
        moveAction= playerControls.Player.Move;
        sprintAction=playerControls.Player.Sprint;
        crouchAction= playerControls.Player.Crouch;

    }
    void OnEnable()
    {
        playerControls.Enable();
        moveAction.performed+=OnMove;
        moveAction.canceled+=OnMove;
        sprintAction.performed+=OnSprint;
        sprintAction.canceled+=OnSprint;
        crouchAction.performed+=OnCrouch;
        crouchAction.canceled+=OnCrouch;


    }
    void OnDisable()
    {  
        playerControls.Disable();
        moveAction.performed-=OnMove;
        moveAction.canceled-=OnMove;
        sprintAction.performed-=OnSprint;
        sprintAction.canceled-=OnSprint;
        crouchAction.performed -=OnCrouch;
        crouchAction.canceled-=OnCrouch;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        isSprinting=context.performed;
    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        isCrouching=context.performed;
    }
}
