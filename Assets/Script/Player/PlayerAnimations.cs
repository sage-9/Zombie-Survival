using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] Player3rdPersonMovement animationData;
    [SerializeField]private Animator animator;

    void Start()
    {
        if(animationData!=null) animationData.speedChanges+=SpeedParameterChages;
        PlayerInput.OnCrouch+=CrouchAnimation;
        PlayerInput.OnAttack+=AttackAnimation;
    }

    void OnDisable()
    {
        if (animationData != null) animationData.speedChanges-=SpeedParameterChages;
        PlayerInput.OnAttack-=AttackAnimation;
        PlayerInput.OnCrouch-=CrouchAnimation;       
    }
    void SpeedParameterChages(float speed)
    {
        animator.SetFloat("speed",speed);
    }
    void CrouchAnimation (bool isCrouching)
    {
        animator.SetBool("isCrouching",isCrouching);
    }
    void AttackAnimation()
    {
        animator.SetTrigger("Attack");
    }
}
