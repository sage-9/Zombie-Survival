using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Animator anim;    

    void Start()
    {
        currentHealth = maxHealth;
       
    }
    

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("TakeDamage");        
        Debug.Log($"zombie took damage! Health: {currentHealth}");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("zombie Died!");
        Destroy(gameObject,0.1f);
        // I will Add death logic here (restart level, game over screen, etc.)
    }
}
