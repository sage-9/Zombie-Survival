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
    private InputAction attackAction;
    public Vector2 moveInput;
    public bool isSprinting;
    public bool isCrouching;
    public bool isAttacking;
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerControls=new PlayerControls();
        moveAction= playerControls.Player.Move;
        sprintAction=playerControls.Player.Sprint;
        crouchAction= playerControls.Player.Crouch;
        attackAction= playerControls.Player.Attack;

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
        attackAction.performed+=OnAttack;
        attackAction.canceled+=OnAttack;


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
        attackAction.performed-=OnAttack;
        attackAction.canceled-=OnAttack;
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
    public void OnAttack(InputAction.CallbackContext context)
    {
        isAttacking=context.performed;
    }
}
