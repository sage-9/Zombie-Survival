using Unity.Entities.UniversalDelegates;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    Rigidbody[] ragdollBodies;
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ragdollBodies = GetComponentsInChildren<Rigidbody>();
        animator=GetComponent<Animator>();
        DeActivateRagdoll();        
    }
    public void ActivateRagdoll()
    {
        foreach(var rigidbody in ragdollBodies) rigidbody.isKinematic = false;
        animator.enabled = false;
    }
    public void DeActivateRagdoll()
    {
        foreach(var rigidbody in ragdollBodies) rigidbody.isKinematic = true;
        animator.enabled = true;
    }

}
