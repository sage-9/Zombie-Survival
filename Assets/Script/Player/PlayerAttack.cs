using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject weapon;
    public LayerMask Ignorelayer;
    MeleeWeapon weaponStats;
    public float radius=3f;
    Vector3 offset;
    int damage;
    float range;
    float attackTime;
    //Events
    public event Action <Collider,int> OnHit;
    void OnDisable()
    {
        PlayerInput.Instance.OnAttack-=Attack;
    }
    void Start()
    {       
        weaponStats= weapon.GetComponent<MeleeWeapon>();
        PlayerInput.Instance.OnAttack+=Attack;
    }

    // Update is called once per frame
    void Update()
    {
        offset=transform.forward*range;
        damage = weaponStats.damage;
        range = weaponStats.range;
        attackTime= weaponStats.attackSpeed;                  
    }
 
    void Attack()
    {          
        Debug.Log("called");        
        Collider[] EnemiesHit=Physics.OverlapSphere(transform.position+offset,radius,~Ignorelayer);
        foreach(Collider Enemy in EnemiesHit)
        {
            if(Enemy.tag=="Enemy")
            {
                OnHit.Invoke(Enemy,damage);
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position+offset,radius);
    }    
}

