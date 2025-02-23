using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public Transform TargetPos;
    public float followDistance;
    public NavMeshAgent agent;
    public Animator animator;
    public Rigidbody rb;
    float speed;
    float turnSmoothTime=5f;
    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(TargetPos.position,transform.position);
        animator.SetFloat("Speed",speed);
        animator.SetFloat("Distance",distance);        
        
        
        if (distance <= followDistance)
        {
            speed=agent.speed;
            agent.SetDestination(TargetPos.position);

            if(distance <=agent.stoppingDistance)
            {
                speed=0;
                FaceTarget();
                animator.SetTrigger("Attack");
            }
        }
        else
        {
            speed=0;
        }
    }
    void FaceTarget()
    {
        Vector3 direction = (TargetPos.position - transform.position).normalized;
        Quaternion lookRotation= Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation,Time.deltaTime * turnSmoothTime);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,followDistance);
    }
}
