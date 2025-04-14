using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public int maxArmor = 50;
    public int currentArmor;

    Collider coll;

    public static event Action<float> OnHealthChanged;
    public static event Action<float> OnArmourChanged;

    void OnEnable()
    {
        EnemyAttack.onDamage+=TakeDamage;
    }
    void OnDisable()
    {
        EnemyAttack.onDamage-=TakeDamage;        
    }

    void Start()
    {
        coll=GetComponent<Collider>();
        currentHealth = maxHealth;
        currentArmor = maxArmor;
    }


    public void TakeDamage(int damage,Collider playerCollider)
    {
        if(coll==playerCollider)
        {
            if (currentArmor > 0)
            {
                int armorAbsorb = Mathf.Min(currentArmor, damage);
                currentArmor -= armorAbsorb;
                damage -= armorAbsorb;
            }
            currentHealth -= damage;
            OnHealthChanged?.Invoke(currentHealth);
            OnArmourChanged?.Invoke(currentArmor);          
            if (currentHealth <= 0)
            {
                Die();
            }
        }
        
    }

    void Die()
    {
        Debug.Log("Player Died!");
        // I will Add death logic here (restart level, game over screen, etc.)
    }
}
