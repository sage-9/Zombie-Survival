using UnityEngine;

public class Player3rdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator anim;
    public Transform cam;


    public float speed;

    
    float horizontal;
    float vertical;

    
    // Update is called once per frame
    void Update()
    {
        anim.SetBool("IsWalking",false);
        Vector3 rotateDirection = new Vector3(0,cam.eulerAngles.y,0);
        Quaternion rotateAngles= Quaternion.Euler(rotateDirection);        
        horizontal=Input.GetAxis("Horizontal");
        vertical=Input.GetAxis("Vertical");
        Vector3 direction= new Vector3 (horizontal,0,vertical).normalized;
        direction=transform.TransformDirection(direction);
        
        if(direction.magnitude>0.01)
        {
            anim.SetBool("IsWalking",true);
            transform.localRotation = rotateAngles;
            controller.Move(direction*speed*Time.deltaTime);
        }
        
        
    }
} 
