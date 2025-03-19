using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public int maxArmor = 50;
    public int currentArmor;

    Collider coll;

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
            Debug.Log($"Player took damage! Health: {currentHealth}, Armor: {currentArmor}");
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
