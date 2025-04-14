using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Vector3 offset;
    bool isMelee;
    //Events
    public static event Action<Vector3,Vector3> OnMeleeAttack;    

    void Attack() 
    {
        OnMeleeAttack?.Invoke(transform.position,transform.forward);    
    }
    
}

