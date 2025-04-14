using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Animator anim;
    Collider myCollider;

    public static event Action<float> OnEnemyHealthChanged;
    public static event Action<GameObject> OnDie;

    void OnEnable()
    {
        myCollider = GetComponent<Collider>();
        currentHealth = maxHealth;
        MeleeWeapon.OnHit += TakeDamage;
    }
    void OnDisable()
    {
        OnDie?.Invoke(gameObject);
        
        MeleeWeapon.OnHit -= TakeDamage;
    }

    public void TakeDamage(Collider hitObject, int damage)
    {
        if (hitObject == myCollider)
        {
            currentHealth -= damage;
            anim.SetTrigger("TakeDamage");
            OnEnemyHealthChanged?.Invoke(currentHealth);
            
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        anim.SetTrigger("IsDead");
        Destroy(gameObject, 0.75f);
    }
}
