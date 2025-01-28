using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player3rdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    private PlayerInput playerInput;
    Vector2 moveInput; 
    bool isSprinting; 
    bool isCrouching;
    public Animator anim;
    public Transform cam;
     float speed;
    public float walkSpeed;
    public float sprintSpeed; 
    public float gravity;
    public float crouchHeight;
    public float normalHeight;
    
    float turnSmoothVelocity;
    float turnSmoothTime=0.1f;   

    void Start() 
    {
        playerInput = GetComponent<PlayerInput>();
        if (playerInput == null) Debug.LogError("PlayerInput component not found on this GameObject!");
        
    }    
    // Update is called once per frame
    void Update()
    {
        if (playerInput == null) return;    

        moveInput=playerInput.moveInput;
        isSprinting=playerInput.isSprinting;
        isCrouching=playerInput.isCrouching;
        speed=isSprinting?sprintSpeed:walkSpeed;

        calculateGravity();
        Crouch();

        Move(calculateDirection(moveInput),speed);

        anim.SetFloat("speed",moveInput.magnitude*speed);
        anim.SetBool("isCrouching",isCrouching);       
    }

    Vector3 calculateDirection(Vector2 moveInput)
    {
        Vector3 direction= new Vector3 (moveInput.x,0,moveInput.y).normalized;
        if(direction.magnitude>0.01)
        {
            float targetAngle = Mathf.Atan2(direction.x,direction.z)*Mathf.Rad2Deg+cam.eulerAngles.y;
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

    void Crouch()
    {
        controller.height=isCrouching?crouchHeight:normalHeight;
    }
   
}
