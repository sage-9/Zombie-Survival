using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player3rdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    private PlayerInput playerInput;
    Vector2 moveInput; 
    bool isSprinting; 
    public Animator anim;
    public Transform cam;
     float speed;
    public float walkSpeed;
    public float sprintSpeed;    
    float horizontal;
    float vertical;
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

        speed=isSprinting?sprintSpeed:walkSpeed;

        Move(calculateDirection(moveInput),speed);

        anim.SetFloat("speed",moveInput.magnitude*speed);       
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
}
