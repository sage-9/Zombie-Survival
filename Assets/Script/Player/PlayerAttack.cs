using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerInput playerInput;
    public GameObject weapon;
    public Animator anim;
    public LayerMask Ignorelayer;
    MeleeWeaponStats weaponStats;
    bool isReadyForAttack;
    public float radius=3f;
    Vector3 offset;
    int damage;
    float range;
    float attackTime;
    bool onAttack;
    bool isAttacking;
    bool isReadyForAttacking;
    bool hasAttacked;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {       
        weaponStats= weapon.GetComponent<MeleeWeaponStats>();
        playerInput = GetComponent<PlayerInput>();
        isAttacking=false;
        hasAttacked=true;
        if (playerInput == null) Debug.LogError("PlayerInput component not found on this GameObject!");
                
    }

    // Update is called once per frame
    void Update()
    {
        offset = transform.forward*weaponStats.range;
        damage = weaponStats.damage;
        range = weaponStats.range;
        attackTime= weaponStats.attackSpeed;
        onAttack=playerInput.isAttacking;
        isReadyForAttacking=!isAttacking && hasAttacked;
        anim.SetBool("isAttacking",onAttack);
        if(onAttack&&isReadyForAttacking)
        {
           Attack();            
        }                      
    }
 
    void Attack()
    {   
        isAttacking=true;
        Debug.Log("called");
        hasAttacked=false;
        StartCoroutine("weaponCooldown");
        if(Physics.SphereCast(transform.position,radius,Vector3.forward,out RaycastHit hitInfo,range,~Ignorelayer))
        {
            Debug.Log("fired");
            GameObject gameObject=hitInfo.collider.GameObject();
            gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
        isAttacking=false;         
    }
    IEnumerator weaponCooldown()
    {
        yield return new WaitForSeconds(attackTime);
        hasAttacked=true;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position+offset,radius);
    }    
}

