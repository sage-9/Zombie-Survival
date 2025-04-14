using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    Rigidbody rb;
    Collider coll;
    public float force;
    public float waitTime;
    float timer;

    void OnEnable()
    {
        MeleeWeapon.PassPlayerPosition+=ApplyKnockbackEffect;        
    }
    void OnDisable()
    {
        MeleeWeapon.PassPlayerPosition-=ApplyKnockbackEffect;        
    }

    void Start ()
    {
        rb = GetComponent<Rigidbody> ();
        coll= GetComponent<Collider> ();
    }
    void Update ()
    {
        timer-=Time.deltaTime;
        if (rb.linearVelocity.magnitude<=0.1&&timer<=0)
        {
            DeActivateRigidBody();
        }

    }

    void ApplyKnockbackEffect(Collider enemy,Vector3 playerPosition)
    {        
        Vector3 direction=-transform.forward;
        if(enemy==coll)
        {
            rb.isKinematic = false;
            rb.AddForce(force*direction,ForceMode.Impulse);
            timer=waitTime;
        }       
    }
    void DeActivateRigidBody()
    {
        rb.isKinematic=true;
    }
}
