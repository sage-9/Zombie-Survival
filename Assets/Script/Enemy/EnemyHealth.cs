using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Animator anim;
    PlayerAttack PlayerAttack; 
    Ragdoll ragdoll;
    Collider myCollider;

    void OnEnable()
    {
        ragdoll=GetComponent<Ragdoll>();
        myCollider=GetComponent<Collider>();
        currentHealth = maxHealth;
        PlayerAttack.OnHit+=TakeDamage;
       
    }
    void OnDisable()
    {
        PlayerAttack.OnHit -=TakeDamage;
    }

    public void TakeDamage(Collider hitObject,int damage)
    {
        if(hitObject==myCollider)
        {
            currentHealth -= damage;
            anim.SetTrigger("TakeDamage");        
            Debug.Log($"zombie took damage! Health: {currentHealth}");
        }
        if (currentHealth <= 0)
        {
            Die();
        }        
    }

    void Die()
    {
        Debug.Log("zombie Died!");
        anim.SetTrigger("IsDead");
        Destroy(gameObject,3.0f);      
    }
}
