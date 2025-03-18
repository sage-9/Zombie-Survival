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

    public delegate void MoveAction(Vector2 direction);
    public static event MoveAction OnMove;
    public delegate void AttackAction();
    public static event AttackAction OnAttack;
    public delegate void SprintAction(bool isPressed);
    public static event SprintAction OnSprint;
    public delegate void CrouchAction(bool isPressed);
    public static event SprintAction OnCrouch;


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

        moveAction.performed+=OnMovePerformed;
        moveAction.canceled+=OnMoveCanceled;

        sprintAction.performed+=OnSprintPerformed;
        sprintAction.canceled+=OnSprintCanceled;

        crouchAction.performed+=OnCrouchPerformed;
        crouchAction.canceled+=OnCrouchCanceled;

        attackAction.performed+=OnAttackTriggered;
        
    }
    void OnDisable()
    {  
        playerControls.Disable();

        moveAction.performed-=OnMovePerformed;
        moveAction.canceled-=OnMoveCanceled;

        sprintAction.performed-=OnSprintPerformed;
        sprintAction.canceled-=OnSprintCanceled;

        crouchAction.performed -=OnCrouchPerformed;
        crouchAction.canceled-=OnCrouchCanceled;

        attackAction.performed-=OnAttackTriggered;
        
    }
    void OnMovePerformed(InputAction.CallbackContext context)
    {
        OnMove?.Invoke(context.ReadValue<Vector2>());
    }
    void OnMoveCanceled (InputAction.CallbackContext context)
    {
        OnMove?.Invoke(Vector2.zero);
    }
    public void OnSprintPerformed(InputAction.CallbackContext context)
    {        
        OnSprint?.Invoke(true);
    }
    public void OnSprintCanceled(InputAction.CallbackContext context)
    {
        OnSprint?.Invoke(false);
    }
    public void OnCrouchPerformed(InputAction.CallbackContext context)
    {
        OnCrouch?.Invoke(true);
    }
    public void OnCrouchCanceled(InputAction.CallbackContext context)
    {
        OnCrouch?.Invoke(false);
    }
    public void OnAttackTriggered(InputAction.CallbackContext context)
    {
        OnAttack?.Invoke();
    }
}
