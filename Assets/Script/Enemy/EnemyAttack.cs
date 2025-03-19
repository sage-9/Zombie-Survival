using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Analytics;

public class EnemyAttack : MonoBehaviour
{
    public int attackDamage;
    Vector3 offset;
    public float attackRange;
    public float radius;
    public float AttackTime;
    bool isAttacking;
    bool isReadytoAttack;
    public LayerMask enemyLayer;
    Vector3 heightoffset=new Vector3(0, 0.84f,0);

    public delegate void PlayerDamage(int damage,Collider player);
    public static PlayerDamage onDamage;
    void OnEnable()
    {
        EnemyFollow.OnZombieAttack+=Attack;        
    }
    void OnDisable()
    {
        EnemyFollow.OnZombieAttack-=Attack;
    }
    void Start()
    {
        isReadytoAttack=true;
        isAttacking=false;
    }
    void Update()
    {
        offset=transform.forward*attackRange+heightoffset;       
        
    }
    void Attack()
    {
        if(isReadytoAttack && !isAttacking)
        {
            isAttacking=true;
            Debug.Log("invoked");
            Collider[] playerHit=Physics.OverlapSphere(transform.position+offset,radius,~enemyLayer);
            foreach(Collider player in playerHit)
            {
                if(player.CompareTag("Player"))
                {
                    onDamage?.Invoke(attackDamage,player);
                }
            }  
            isAttacking=false;
            isReadytoAttack=false;
            StartCoroutine(StartTimer(AttackTime));   
        }
              
    }
    IEnumerator StartTimer(float time)
    {
        yield return new WaitForSeconds(time);
        isReadytoAttack=true;              
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position+offset,radius);
    }    
}
