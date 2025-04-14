using Unity.VisualScripting;
using UnityEngine;

public class ArmourLogic : MonoBehaviour
{
    float scaleAmount;
    float HealthAmount;
    float healthMax=50;
    public Transform healthBar;

    void OnEnable()
    {
        PlayerHealth.OnArmourChanged+=onHealthChange;

    }
    void OnDisable()
    {
        PlayerHealth.OnArmourChanged -=onHealthChange;
    }


    void Start()
    {
        HealthAmount=healthMax;
    }
    void onHealthChange(float health)
    {
        HealthAmount = health;
        updateScale(calculateScale());
    }
    float calculateScale()
    {
        scaleAmount=HealthAmount/healthMax;
        return scaleAmount;
    }

    void updateScale(float scaleAmount)
    {
        healthBar.localScale=new Vector3(scaleAmount, healthBar.localScale.y,healthBar.localScale.z);
    }
}
