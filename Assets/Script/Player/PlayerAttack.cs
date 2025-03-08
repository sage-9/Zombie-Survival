using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject weapon;
    public LayerMask Ignorelayer;
    MeleeWeapon weaponStats;
    Collider meleeWeaponCollider;
    public float radius=3f;
    Vector3 offset;
    int damage;
    float range;
    float attackTime;
    bool isReadyForAttack;
    bool isAttacking;
    bool hasAttacked;
    void OnEnable()
    {
        PlayerInput.Instance.OnAttack+=Attack;                
    }
    void OnDisable()
    {
        PlayerInput.Instance.OnAttack-=Attack;
    }
    void Start()
    {       
        weaponStats= weapon.GetComponent<MeleeWeapon>();
        meleeWeaponCollider=weapon.GetComponent<Collider>();
        isAttacking=false;
        hasAttacked=true;
    }

    // Update is called once per frame
    void Update()
    {
        offset = transform.forward*weaponStats.range;
        damage = weaponStats.damage;
        range = weaponStats.range;
        attackTime= weaponStats.attackSpeed;
      
        isReadyForAttack=!isAttacking && hasAttacked;
        meleeWeaponCollider.enabled=isAttacking;                   
    }
 
    void Attack()
    {          
        isAttacking=true;
        hasAttacked=false;
        Debug.Log("called");
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

