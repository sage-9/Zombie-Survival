using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
public class Player3rdPersonMovement : MonoBehaviour
{
    //Player Movement 
    public CharacterController controller;
    public Transform camDirection;
    Vector3 moveDirection;
    float speed;
    bool isSprinting;
        //rotation variables    
    float turnSmoothVelocity;
    float turnSmoothTime=0.1f;   
    public float gravity;

    //movement speeds
    public float walkSpeed;
    public float crouchSpeed;
    public float sprintSpeed;

    //Crouch variables
    public float crouchHeight;
    public float normalHeight;
    bool isCrouching;

    //events & delegates
    public delegate void SpeedChanges(float speed);
    public SpeedChanges speedChanges;

    void Start() 
    {
        PlayerInput.OnMove+=TakeMovementInput;
        PlayerInput.OnCrouch+=HandleCrouch;
        PlayerInput.OnSprint+=HandleSprint;       
    }
    void OnDisable()
    {
        PlayerInput.OnMove-=TakeMovementInput;
        PlayerInput.OnCrouch-=HandleCrouch;
        PlayerInput.OnSprint-=HandleSprint;
    }
    // Update is called once per frame
    void Update()
    {
        calculateGravity();
        CalculateSpeed(isSprinting,isCrouching);           
        Move(calculateDirection(moveDirection),speed);           
    }

    void TakeMovementInput(Vector2 direction)
    {
        moveDirection=direction;
        
    }
    void CalculateSpeed(bool isSprinting,bool isCrouching)
    {
        if(moveDirection.magnitude<0.01)speed=0;
        else
        {
            if(!isCrouching) speed=isSprinting?sprintSpeed:walkSpeed;
            else speed = crouchSpeed;
        }       
        speedChanges?.Invoke(speed);        
    }
    Vector3 calculateDirection(Vector2 moveInput)
    {
        Vector3 direction= new Vector3 (moveInput.x,0,moveInput.y).normalized;
        if(direction.magnitude>0.01)
        {
            float targetAngle = Mathf.Atan2(direction.x,direction.z)*Mathf.Rad2Deg+camDirection.eulerAngles.y;
            float angle= Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle,ref turnSmoothVelocity,turnSmoothTime);
            transform.rotation=Quaternion.Euler(0,angle,0);
            Vector3 movDir=Quaternion.Euler(0,targetAngle,0)*Vector3.forward;  
            return movDir;                                 
        }
        return direction;
    }
    void Move(Vector3 movDir,float speed)
    {
        controller.Move(movDir*speed*Time.deltaTime);
    }
    void calculateGravity ()
    {
        bool isGrounded=controller.isGrounded;
        Vector3 velocity = new Vector3(0,0,0);
        if (!isGrounded)
        {
            velocity.y+=gravity*Time.deltaTime;
            controller.Move(velocity*Time.deltaTime);                                    
        }
        else velocity.y=0;
    }
    void HandleSprint(bool Sprinting)
    {
        isSprinting=Sprinting;
    }
    void HandleCrouch(bool Crouching)
    {
        isCrouching=Crouching;
        controller.height=Crouching?crouchHeight:normalHeight;
    }    
}
