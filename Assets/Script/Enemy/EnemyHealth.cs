using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemyManager enemyManager;
    public int maxHealth = 100;
    public int currentHealth;
    public Animator anim;
    Collider myCollider;

    public static event Action<float> OnEnemyHealthChanged;

    void OnEnable()
    {
        enemyManager.ActiveEnemies.Add(gameObject);
        myCollider = GetComponent<Collider>();
        currentHealth = maxHealth;
        MeleeWeapon.OnHit += TakeDamage;
    }
    void OnDisable()
    {
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
        enemyManager.ActiveEnemies.Remove(gameObject);
        anim.SetTrigger("IsDead");
        Destroy(gameObject, 0.75f);
    }
}
