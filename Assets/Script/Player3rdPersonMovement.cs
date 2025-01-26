using UnityEngine;

public class Player3rdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    float horizontal;
    float vertical;

    
    // Update is called once per frame
    void Update()
    {
        horizontal=Input.GetAxis("Horizontal");
        vertical=Input.GetAxis("Vertical");
        Vector3 direction= new Vector3 (horizontal,0,vertical);
        controller.Move(direction*speed*Time.deltaTime);
        
    }
} 
