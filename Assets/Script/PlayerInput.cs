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
    public Vector2 moveInput;
    public bool isSprinting;



    void Awake()
    {
        playerControls=new PlayerControls();
        moveAction= playerControls.Player.Move;
        sprintAction=playerControls.Player.Sprint;

    }
    void OnEnable()
    {
        playerControls.Enable();
        moveAction.performed+=OnMove;
        moveAction.canceled+=OnMove;
        sprintAction.performed+=OnSprint;
        sprintAction.canceled+=OnSprint;

    }
    void OnDisable()
    {  
        playerControls.Disable();
        moveAction.performed-=OnMove;
        moveAction.canceled-=OnMove;
        sprintAction.performed-=OnSprint;
        sprintAction.canceled-=OnSprint;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        isSprinting=context.performed;
    }
}
