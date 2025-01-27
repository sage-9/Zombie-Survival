using Unity.Mathematics;
using UnityEngine;

public class Player3rdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator anim;
    public Transform cam;
    public float speed;    
    float horizontal;
    float vertical;
    float turnSmoothVelocity;
    float turnSmoothTime=0.1f;

    
    // Update is called once per frame
    void Update()
    {
        anim.SetBool("IsWalking",false);
        horizontal=Input.GetAxisRaw("Horizontal");
        vertical=Input.GetAxisRaw("Vertical");
        Vector3 direction= new Vector3 (horizontal,0,vertical).normalized;       
        if(direction.magnitude>0.01)
        {
            float targetAngle = Mathf.Atan2(direction.x,direction.z)*Mathf.Rad2Deg+cam.eulerAngles.y;
            float angle= Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle,ref turnSmoothVelocity,turnSmoothTime);
            transform.rotation=Quaternion.Euler(0,angle,0);
            Vector3 movDir=Quaternion.Euler(0,targetAngle,0)*Vector3.forward;                      
            controller.Move(movDir.normalized*speed*Time.deltaTime);
            anim.SetBool("IsWalking",true);  
        }
        
        
    }
} 
