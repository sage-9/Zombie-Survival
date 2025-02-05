using UnityEngine;

public class DamageTester : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public int damageAmount = 20;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            playerHealth.TakeDamage(damageAmount);
        }
    }
}

